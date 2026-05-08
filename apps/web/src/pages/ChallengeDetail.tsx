import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import ComparisonViewer from '../components/ComparisonViewer';
import Layout from '../components/Layout';
import { ArrowLeft, Loader2 } from 'lucide-react';
import { challengesApi, type ChallengeImplementationDto } from '../lib/api';

const MONACO_LANGUAGE: Record<string, string> = {
  'C#': 'csharp',
  Java: 'java',
  JavaScript: 'javascript',
  TypeScript: 'typescript',
  Python: 'python',
};

function toMonacoLang(languageName: string): string {
  return MONACO_LANGUAGE[languageName] ?? languageName.toLowerCase();
}

const ChallengeDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: challenge, isLoading, error } = useQuery({
    queryKey: ['challenge', id],
    queryFn: () => challengesApi.getById(id!),
    enabled: !!id,
  });

  const implementations = challenge?.implementations ?? [];
  const [leftIdx, setLeftIdx] = useState(0);
  const [rightIdx, setRightIdx] = useState(1);

  const leftImpl: ChallengeImplementationDto | undefined = implementations[leftIdx];
  const rightImpl: ChallengeImplementationDto | undefined = implementations[rightIdx];

  const displayLanguage = leftImpl && rightImpl
    ? leftImpl.languageName === rightImpl.languageName
      ? leftImpl.languageName
      : `${leftImpl.languageName} / ${rightImpl.languageName}`
    : '';

  return (
    <Layout>
      <div className="space-y-6 h-[calc(100vh-160px)] flex flex-col">
        <button
          onClick={() => navigate('/')}
          className="flex items-center gap-2 text-gray-400 hover:text-white transition-colors w-fit"
        >
          <ArrowLeft size={18} /> Back to Challenges
        </button>

        {isLoading && (
          <div className="flex items-center justify-center flex-1">
            <Loader2 className="h-8 w-8 animate-spin text-primary" />
          </div>
        )}

        {error && (
          <div className="glass-panel p-6 rounded-2xl border border-red-500/20 text-red-400 text-sm">
            Failed to load challenge.
          </div>
        )}

        {/* Implementation selector — only shown when there are more than 2 */}
        {implementations.length > 2 && (
          <div className="flex items-center gap-4 text-sm">
            <div className="flex items-center gap-2">
              <span className="text-gray-500">Left:</span>
              <select
                value={leftIdx}
                onChange={e => setLeftIdx(Number(e.target.value))}
                className="bg-background border border-card-border rounded-lg px-3 py-1.5 text-sm focus:outline-none focus:ring-2 focus:ring-primary/50"
              >
                {implementations.map((impl, i) => (
                  <option key={impl.id} value={i}>{impl.dialectName}</option>
                ))}
              </select>
            </div>
            <div className="flex items-center gap-2">
              <span className="text-gray-500">Right:</span>
              <select
                value={rightIdx}
                onChange={e => setRightIdx(Number(e.target.value))}
                className="bg-background border border-card-border rounded-lg px-3 py-1.5 text-sm focus:outline-none focus:ring-2 focus:ring-primary/50"
              >
                {implementations.map((impl, i) => (
                  <option key={impl.id} value={i}>{impl.dialectName}</option>
                ))}
              </select>
            </div>
          </div>
        )}

        {challenge && leftImpl && rightImpl && (
          <div className="flex-1">
            <ComparisonViewer
              challengeTitle={challenge.title}
              description={challenge.description}
              leftLanguage={toMonacoLang(leftImpl.languageName)}
              rightLanguage={toMonacoLang(rightImpl.languageName)}
              displayLanguage={displayLanguage}
              leftTitle={leftImpl.dialectName}
              rightTitle={rightImpl.dialectName}
              leftCode={leftImpl.starterCode}
              rightCode={rightImpl.starterCode}
              leftReferenceSolution={leftImpl.referenceSolution}
              rightReferenceSolution={rightImpl.referenceSolution}
              leftFeatures={leftImpl.syntaxFeatures}
              rightFeatures={rightImpl.syntaxFeatures}
            />
          </div>
        )}

        {challenge && implementations.length < 2 && (
          <div className="glass-panel p-6 rounded-2xl text-gray-400 text-sm">
            This challenge does not have two implementations to compare yet.
          </div>
        )}
      </div>
    </Layout>
  );
};

export default ChallengeDetail;
