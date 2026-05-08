import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { QueryClient, QueryClientProvider, useQuery } from '@tanstack/react-query';
import Layout from './components/Layout';
import ChallengeCard from './components/ChallengeCard';
import ChallengeDetail from './pages/ChallengeDetail';
import Settings from './pages/Settings';
import ErrorBoundary from './components/ErrorBoundary';
import { challengesApi, type ChallengeDto } from './lib/api';
import { Search, Filter, Cpu, Zap, TrendingUp, Loader2 } from 'lucide-react';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 60_000,
      retry: 1,
    },
  },
});

function Home() {
  const { data, isLoading, error } = useQuery({
    queryKey: ['challenges'],
    queryFn: () => challengesApi.getAll(),
  });

  return (
    <div className="max-w-6xl mx-auto space-y-12">
      <header className="space-y-4">
        <h1 className="text-4xl font-extrabold tracking-tight">Welcome back, Developer.</h1>
        <p className="text-gray-400 text-lg">Pick a challenge and master your favorite ecosystem.</p>
      </header>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <StatCard icon={<Cpu className="text-blue-400" />} label="Challenges Solved" value="0" delta="Get started" />
        <StatCard icon={<Zap className="text-yellow-400" />} label="Current Streak" value="0 days" delta="Build a streak" />
        <StatCard icon={<TrendingUp className="text-green-400" />} label="Rank" value="--" delta="Global Leaderboard" />
      </div>

      <div className="flex flex-col md:flex-row gap-4 items-center justify-between p-4 glass-panel rounded-2xl">
        <div className="relative w-full md:w-96">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-500 h-5 w-5" />
          <input
            type="text"
            placeholder="Search challenges, languages, or tags..."
            className="w-full bg-background border border-card-border rounded-xl py-3 pl-10 pr-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/50 transition-all"
          />
        </div>
        <div className="flex gap-3">
          <button className="flex items-center gap-2 px-4 py-3 border border-card-border rounded-xl text-sm font-medium hover:bg-secondary transition-colors">
            <Filter size={18} /> Filters
          </button>
          <select className="bg-background border border-card-border rounded-xl px-4 py-3 text-sm font-medium focus:outline-none cursor-pointer hover:bg-secondary transition-colors">
            <option>Newest First</option>
            <option>Most Popular</option>
            <option>Difficulty: Low to High</option>
          </select>
        </div>
      </div>

      {isLoading && (
        <div className="flex items-center justify-center py-12">
          <Loader2 className="h-8 w-8 animate-spin text-primary" />
        </div>
      )}

      {error && (
        <div className="glass-panel p-6 rounded-2xl border border-red-500/20 text-red-400 text-sm">
          Failed to load challenges. Make sure the API server is running.
        </div>
      )}

      {data && (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {data.items.map((challenge: ChallengeDto) => (
            <Link
              key={challenge.id}
              to={`/challenge/${challenge.id}`}
              className="block transform hover:scale-[1.02] transition-transform"
            >
              <ChallengeCard
                title={challenge.title}
                description={challenge.description}
                difficulty={challenge.difficulty}
                category={challenge.categoryName}
                tags={challenge.tags}
              />
            </Link>
          ))}
        </div>
      )}
    </div>
  );
}

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <ErrorBoundary>
          <Routes>
            <Route path="/" element={<Layout><Home /></Layout>} />
            <Route path="/challenges" element={<Layout><Home /></Layout>} />
            <Route path="/challenge/:id" element={<ChallengeDetail />} />
            <Route path="/settings" element={<Layout><Settings /></Layout>} />
          </Routes>
        </ErrorBoundary>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

const StatCard = ({
  icon,
  label,
  value,
  delta,
}: {
  icon: React.ReactNode;
  label: string;
  value: string;
  delta: string;
}) => (
  <div className="glass-panel p-6 rounded-2xl space-y-4">
    <div className="flex items-center gap-3 text-gray-400">
      {icon}
      <span className="text-sm font-medium">{label}</span>
    </div>
    <div className="flex items-end justify-between">
      <span className="text-3xl font-bold tracking-tight">{value}</span>
      <span className="text-xs font-bold text-primary px-2 py-1 bg-primary/10 rounded-md">{delta}</span>
    </div>
  </div>
);

export default App;
