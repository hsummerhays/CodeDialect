import React from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import ComparisonViewer from '../components/ComparisonViewer';
import Layout from '../components/Layout';
import { ArrowLeft, Loader2 } from 'lucide-react';
import { challengesApi } from '../lib/api';

const MONACO_LANGUAGE: Record<string, string> = {
  'C#': 'csharp',
  Java: 'java',
  JavaScript: 'javascript',
  TypeScript: 'typescript',
  Python: 'python',
};

const ChallengeDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: challenge, isLoading, error } = useQuery({
    queryKey: ['challenge', id],
    queryFn: () => challengesApi.getById(id!),
    enabled: !!id,
  });

  const [leftImpl, rightImpl] = challenge?.implementations ?? [];

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

        {challenge && leftImpl && rightImpl && (
          <div className="flex-1">
            <ComparisonViewer
              challengeTitle={challenge.title}
              description={challenge.description}
              language={MONACO_LANGUAGE[leftImpl.languageName] ?? leftImpl.languageName.toLowerCase()}
              displayLanguage={leftImpl.languageName}
              leftTitle={leftImpl.dialectName}
              rightTitle={rightImpl.dialectName}
              leftCode={leftImpl.starterCode}
              rightCode={rightImpl.starterCode}
            />
          </div>
        )}

        {challenge && (!leftImpl || !rightImpl) && (
          <div className="glass-panel p-6 rounded-2xl text-gray-400 text-sm">
            This challenge does not have two implementations to compare yet.
          </div>
        )}
      </div>
    </Layout>
  );
};

export default ChallengeDetail;
