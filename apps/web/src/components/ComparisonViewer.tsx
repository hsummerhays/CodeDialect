import React, { useRef, useState } from 'react';
import Editor, { DiffEditor, type OnMount } from '@monaco-editor/react';
import { Terminal, Zap, Info, ChevronRight, Share2, Code2, Columns, GitCompare, Eye, EyeOff } from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';

type MonacoEditorInstance = Parameters<OnMount>[0];

interface ComparisonViewerProps {
  leftCode: string;
  rightCode: string;
  leftTitle: string;
  rightTitle: string;
  leftLanguage: string;
  rightLanguage: string;
  displayLanguage: string;
  challengeTitle: string;
  description: string;
  leftReferenceSolution?: string;
  rightReferenceSolution?: string;
  leftFeatures?: string[];
  rightFeatures?: string[];
}

const ComparisonViewer: React.FC<ComparisonViewerProps> = ({
  leftCode,
  rightCode,
  leftTitle,
  rightTitle,
  leftLanguage,
  rightLanguage,
  displayLanguage,
  challengeTitle,
  description,
  leftReferenceSolution,
  rightReferenceSolution,
  leftFeatures = [],
  rightFeatures = [],
}) => {
  const [viewMode, setViewMode] = useState<'side-by-side' | 'diff'>('side-by-side');
  const [showSolutions, setShowSolutions] = useState(false);
  const leftEditorRef = useRef<MonacoEditorInstance | null>(null);
  const rightEditorRef = useRef<MonacoEditorInstance | null>(null);
  const isSyncing = useRef(false);

  const handleLeftMount: OnMount = (editor) => {
    leftEditorRef.current = editor;
    editor.onDidScrollChange((e) => {
      if (!e.scrollTopChanged || isSyncing.current) return;
      isSyncing.current = true;
      rightEditorRef.current?.setScrollTop(e.scrollTop);
      isSyncing.current = false;
    });
  };

  const handleRightMount: OnMount = (editor) => {
    rightEditorRef.current = editor;
    editor.onDidScrollChange((e) => {
      if (!e.scrollTopChanged || isSyncing.current) return;
      isSyncing.current = true;
      leftEditorRef.current?.setScrollTop(e.scrollTop);
      isSyncing.current = false;
    });
  };

  const leftDisplayCode = showSolutions ? (leftReferenceSolution || leftCode) : leftCode;
  const rightDisplayCode = showSolutions ? (rightReferenceSolution || rightCode) : rightCode;

  return (
    <div className="flex flex-col h-full space-y-4">
      {/* Header Panel */}
      <div className="flex flex-col lg:flex-row lg:items-center justify-between p-6 glass-panel rounded-2xl gap-4">
        <div className="space-y-1">
          <div className="flex items-center gap-3">
            <div className="p-2 rounded-lg bg-primary/10 text-primary">
              <Code2 size={24} />
            </div>
            <div>
              <h2 className="text-2xl font-bold tracking-tight">{challengeTitle}</h2>
              <div className="flex items-center gap-2 mt-1">
                <span className="px-2 py-0.5 text-[10px] font-bold bg-primary/20 text-primary rounded border border-primary/20 uppercase tracking-wider">
                  {displayLanguage}
                </span>
                <span className="text-xs text-gray-500 font-medium">{description}</span>
              </div>
            </div>
          </div>
        </div>

        <div className="flex items-center gap-3 self-end lg:self-center">
          <div className="flex bg-secondary/30 p-1 rounded-xl border border-white/5">
            <button
              onClick={() => setViewMode('side-by-side')}
              className={`flex items-center gap-2 px-3 py-1.5 rounded-lg text-xs font-bold transition-all ${
                viewMode === 'side-by-side' ? 'bg-primary text-white shadow-lg shadow-primary/20' : 'text-gray-400 hover:text-white'
              }`}
            >
              <Columns size={14} /> Side-by-Side
            </button>
            <button
              onClick={() => setViewMode('diff')}
              className={`flex items-center gap-2 px-3 py-1.5 rounded-lg text-xs font-bold transition-all ${
                viewMode === 'diff' ? 'bg-primary text-white shadow-lg shadow-primary/20' : 'text-gray-400 hover:text-white'
              }`}
            >
              <GitCompare size={14} /> View Diff
            </button>
          </div>

          <button
            onClick={() => setShowSolutions(!showSolutions)}
            className={`flex items-center gap-2 px-4 py-2 rounded-xl text-sm font-bold transition-all border ${
              showSolutions
                ? 'bg-amber-500/10 border-amber-500/50 text-amber-500'
                : 'bg-secondary/50 border-white/5 text-gray-400 hover:text-white'
            }`}
          >
            {showSolutions ? <EyeOff size={16} /> : <Eye size={16} />}
            {showSolutions ? 'Hide Solution' : 'Reveal Solution'}
          </button>

          <button className="flex items-center gap-2 px-6 py-2 bg-primary text-white rounded-xl text-sm font-bold shadow-lg shadow-primary/30 hover:scale-105 transition-all active:scale-95 group">
            <Zap size={16} className="group-hover:animate-pulse" /> Run Benchmark
          </button>
        </div>
      </div>

      {/* Evolution Summary Bar */}
      <AnimatePresence>
        {(leftFeatures.length > 0 || rightFeatures.length > 0) && (
          <motion.div
            initial={{ height: 0, opacity: 0 }}
            animate={{ height: 'auto', opacity: 1 }}
            exit={{ height: 0, opacity: 0 }}
            className="grid grid-cols-1 lg:grid-cols-2 gap-4"
          >
            <div className="flex flex-wrap gap-2 px-4">
              {leftFeatures.map(f => (
                <span key={f} className="text-[10px] px-2 py-0.5 rounded-full bg-red-400/10 text-red-400/80 border border-red-400/20 font-bold uppercase tracking-tighter">
                  {f}
                </span>
              ))}
            </div>
            <div className="flex flex-wrap gap-2 px-4 justify-end">
              {rightFeatures.map(f => (
                <span key={f} className="text-[10px] px-2 py-0.5 rounded-full bg-green-400/10 text-green-400/80 border border-green-400/20 font-bold uppercase tracking-tighter">
                  {f}
                </span>
              ))}
            </div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Main Editor Area */}
      <div className="flex-1 min-h-[500px] relative">
        <AnimatePresence mode="wait">
          {viewMode === 'side-by-side' ? (
            <motion.div
              key="side-by-side"
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -10 }}
              className="grid grid-cols-1 lg:grid-cols-2 gap-4 h-full"
            >
              {/* Left Editor */}
              <div className="flex flex-col glass-panel rounded-2xl overflow-hidden border border-white/5 relative group">
                <div className="absolute top-0 left-0 w-1 h-full bg-red-500/50 z-10" />
                <div className="flex items-center justify-between px-4 py-3 bg-white/5 border-b border-white/5">
                  <div className="flex items-center gap-2">
                    <div className="w-2 h-2 rounded-full bg-red-400" />
                    <span className="text-xs font-bold text-gray-400 uppercase tracking-widest">{leftTitle}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    {showSolutions && <span className="text-[9px] font-bold bg-amber-500/20 text-amber-500 px-1.5 py-0.5 rounded">REFERENCE</span>}
                  </div>
                </div>
                <div className="flex-1">
                  <Editor
                    height="100%"
                    theme="vs-dark"
                    language={leftLanguage}
                    value={leftDisplayCode}
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
                      fontFamily: "'Fira Code', monospace",
                      fontLigatures: true,
                    }}
                  />
                </div>
              </div>

              {/* Right Editor */}
              <div className="flex flex-col glass-panel rounded-2xl overflow-hidden border border-primary/20 relative">
                <div className="absolute top-0 left-0 w-1 h-full bg-primary z-10" />
                <div className="flex items-center justify-between px-4 py-3 bg-primary/5 border-b border-primary/10">
                  <div className="flex items-center gap-2">
                    <div className="w-2 h-2 rounded-full bg-primary" />
                    <span className="text-xs font-bold text-primary uppercase tracking-widest">{rightTitle}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    {showSolutions && <span className="text-[9px] font-bold bg-amber-500/20 text-amber-500 px-1.5 py-0.5 rounded">REFERENCE</span>}
                  </div>
                </div>
                <div className="flex-1">
                  <Editor
                    height="100%"
                    theme="vs-dark"
                    language={rightLanguage}
                    value={rightDisplayCode}
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
                      fontFamily: "'Fira Code', monospace",
                      fontLigatures: true,
                    }}
                  />
                </div>
              </div>
            </motion.div>
          ) : (
            <motion.div
              key="diff"
              initial={{ opacity: 0, scale: 0.98 }}
              animate={{ opacity: 1, scale: 1 }}
              exit={{ opacity: 0, scale: 0.98 }}
              className="h-full glass-panel rounded-2xl overflow-hidden border border-white/10"
            >
              <div className="flex items-center justify-between px-4 py-3 bg-white/5 border-b border-white/5">
                <div className="flex items-center gap-4">
                  <div className="flex items-center gap-2">
                    <div className="w-2 h-2 rounded-full bg-red-400/50" />
                    <span className="text-[10px] font-bold text-gray-500 uppercase">{leftTitle}</span>
                  </div>
                  <ChevronRight size={14} className="text-gray-700" />
                  <div className="flex items-center gap-2">
                    <div className="w-2 h-2 rounded-full bg-primary" />
                    <span className="text-[10px] font-bold text-primary uppercase">{rightTitle}</span>
                  </div>
                </div>
                <span className="text-[10px] font-mono text-gray-500 uppercase tracking-widest">Diff Analysis</span>
              </div>
              <div className="h-[calc(100%-45px)]">
                <DiffEditor
                  height="100%"
                  theme="vs-dark"
                  language={leftLanguage}
                  original={leftDisplayCode}
                  modified={rightDisplayCode}
                  options={{
                    originalEditable: false,
                    readOnly: false,
                    renderSideBySide: true,
                    minimap: { enabled: false },
                    fontSize: 14,
                    padding: { top: 16, bottom: 16 },
                    fontFamily: "'Fira Code', monospace",
                  }}
                />
              </div>
            </motion.div>
          )}
        </AnimatePresence>
      </div>

      {/* Footer Info Panel */}
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
              Dialect Engine: <span className="text-white">v1.0.4-beta</span>
            </span>
          </div>
        </div>
        <div className="flex items-center gap-4">
          <button className="flex items-center gap-2 text-gray-400 hover:text-white transition-colors">
            <Share2 size={16} />
            <span>Copy Link</span>
          </button>
          <div className="h-4 w-px bg-white/10" />
          <div className="flex items-center gap-2 text-primary font-bold cursor-help group">
            Analysis Engine Active
            <div className="w-2 h-2 rounded-full bg-primary animate-pulse" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ComparisonViewer;
