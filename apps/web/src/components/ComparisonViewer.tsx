import React, { useRef } from 'react';
import Editor, { type OnMount } from '@monaco-editor/react';
import { Terminal, Zap, Info, ChevronRight, Share2 } from 'lucide-react';
import { motion } from 'framer-motion';

type MonacoEditorInstance = Parameters<OnMount>[0];

interface ComparisonViewerProps {
  leftCode: string;
  rightCode: string;
  leftTitle: string;
  rightTitle: string;
  language: string;
  displayLanguage: string;
  challengeTitle: string;
  description: string;
}

const ComparisonViewer: React.FC<ComparisonViewerProps> = ({
  leftCode,
  rightCode,
  leftTitle,
  rightTitle,
  language,
  displayLanguage,
  challengeTitle,
  description,
}) => {
  const leftEditorRef = useRef<MonacoEditorInstance | null>(null);
  const rightEditorRef = useRef<MonacoEditorInstance | null>(null);

  const syncScrolling = (editor: MonacoEditorInstance, source: 'left' | 'right') => {
    editor.onDidScrollChange((e) => {
      if (!e.scrollTopChanged) return;
      const target = source === 'left' ? rightEditorRef.current : leftEditorRef.current;
      target?.setScrollTop(e.scrollTop);
    });
  };

  const handleLeftMount: OnMount = (editor) => {
    leftEditorRef.current = editor;
    syncScrolling(editor, 'left');
  };

  const handleRightMount: OnMount = (editor) => {
    rightEditorRef.current = editor;
    syncScrolling(editor, 'right');
  };

  return (
    <div className="flex flex-col h-full space-y-4">
      <div className="flex items-center justify-between p-4 glass-panel rounded-2xl">
        <div className="space-y-1">
          <div className="flex items-center gap-2">
            <h2 className="text-xl font-bold tracking-tight">{challengeTitle}</h2>
            <span className="px-2 py-0.5 text-xs font-bold bg-primary/20 text-primary rounded-md uppercase">
              {displayLanguage}
            </span>
          </div>
          <p className="text-sm text-gray-400">{description}</p>
        </div>
        <div className="flex gap-2">
          <button className="flex items-center gap-2 px-4 py-2 bg-secondary/50 hover:bg-secondary rounded-xl text-sm font-medium transition-colors">
            <Share2 size={16} /> Share
          </button>
          <button className="flex items-center gap-2 px-4 py-2 bg-primary text-primary-foreground rounded-xl text-sm font-bold shadow-lg shadow-primary/20 hover:scale-105 transition-transform active:scale-95">
            <Zap size={16} /> Run Benchmark
          </button>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-4 flex-1 min-h-[500px]">
        <motion.div
          initial={{ opacity: 0, x: -20 }}
          animate={{ opacity: 1, x: 0 }}
          className="flex flex-col glass-panel rounded-2xl overflow-hidden border border-white/5"
        >
          <div className="flex items-center justify-between px-4 py-3 bg-white/5 border-b border-white/5">
            <div className="flex items-center gap-2">
              <div className="w-2 h-2 rounded-full bg-red-400/50" />
              <span className="text-xs font-bold text-gray-400 uppercase tracking-widest">{leftTitle}</span>
            </div>
            <span className="text-[10px] font-mono text-gray-600">REFERENCE</span>
          </div>
          <div className="flex-1">
            <Editor
              height="100%"
              theme="vs-dark"
              language={language}
              value={leftCode}
              onMount={handleLeftMount}
              options={{
                minimap: { enabled: false },
                fontSize: 14,
                lineNumbers: 'on',
                roundedSelection: true,
                scrollBeyondLastLine: false,
                readOnly: true,
                automaticLayout: true,
                padding: { top: 16, bottom: 16 },
              }}
            />
          </div>
        </motion.div>

        <motion.div
          initial={{ opacity: 0, x: 20 }}
          animate={{ opacity: 1, x: 0 }}
          className="flex flex-col glass-panel rounded-2xl overflow-hidden border border-primary/20"
        >
          <div className="flex items-center justify-between px-4 py-3 bg-primary/5 border-b border-primary/10">
            <div className="flex items-center gap-2">
              <div className="w-2 h-2 rounded-full bg-primary" />
              <span className="text-xs font-bold text-primary uppercase tracking-widest">{rightTitle}</span>
            </div>
            <span className="text-[10px] font-mono text-primary/60">MODERN</span>
          </div>
          <div className="flex-1">
            <Editor
              height="100%"
              theme="vs-dark"
              language={language}
              value={rightCode}
              onMount={handleRightMount}
              options={{
                minimap: { enabled: false },
                fontSize: 14,
                lineNumbers: 'on',
                roundedSelection: true,
                scrollBeyondLastLine: false,
                readOnly: false,
                automaticLayout: true,
                padding: { top: 16, bottom: 16 },
              }}
            />
          </div>
        </motion.div>
      </div>

      <div className="glass-panel p-4 rounded-2xl flex items-center justify-between text-sm">
        <div className="flex items-center gap-6">
          <div className="flex items-center gap-2">
            <Terminal size={16} className="text-gray-500" />
            <span className="text-gray-400 font-mono">
              Status: <span className="text-green-400">Ready</span>
            </span>
          </div>
          <div className="flex items-center gap-2">
            <Info size={16} className="text-gray-500" />
            <span className="text-gray-400 font-mono">
              Memory: <span className="text-white">-- MB</span>
            </span>
          </div>
        </div>
        <div className="flex items-center gap-2 text-primary font-bold">
          Compare Versions <ChevronRight size={16} />
        </div>
      </div>
    </div>
  );
};

export default ComparisonViewer;
