import React from 'react';
import Layout from './components/Layout';
import ChallengeCard from './components/ChallengeCard';
import { Search, Filter, Cpu, Zap, TrendingUp } from 'lucide-react';

function App() {
  return (
    <Layout>
      <div className="max-w-6xl mx-auto space-y-12">
        {/* Hero Section */}
        <header className="space-y-4">
          <h1 className="text-4xl font-extrabold tracking-tight">Welcome back, Developer.</h1>
          <p className="text-gray-400 text-lg">Pick a challenge and master your favorite ecosystem.</p>
        </header>

        {/* Stats Grid */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <StatCard icon={<Cpu className="text-blue-400" />} label="Challenges Solved" value="12" delta="+2 this week" />
          <StatCard icon={<Zap className="text-yellow-400" />} label="Current Streak" value="5 days" delta="Mastery Tier" />
          <StatCard icon={<TrendingUp className="text-green-400" />} label="Rank" value="Top 5%" delta="Global Leaderboard" />
        </div>

        {/* Filter Bar */}
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

        {/* Challenges Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <ChallengeCard 
            title="String Interpolation Performance" 
            description="Compare C# 8.0 string interpolation with .NET 10 enhanced interpolated string handlers for high-throughput logging."
            difficulty="Intermediate"
            category="Back-End"
            tags={[".NET", "C#", "Performance"]}
          />
          <ChallengeCard 
            title="React Hooks Migration" 
            description="Refactor a complex Class Component with lifecycle methods into a modern Functional Component with custom hooks."
            difficulty="Beginner"
            category="Front-End"
            tags={["React", "JavaScript"]}
          />
          <ChallengeCard 
            title="Distributed Lock with Redis" 
            description="Implement a thread-safe distributed lock using StackExchange.Redis and compare with RedLock algorithm."
            difficulty="Advanced"
            category="Infrastructure"
            tags={["Redis", "Distributed Systems"]}
          />
        </div>
      </div>
    </Layout>
  );
}

const StatCard = ({ icon, label, value, delta }: { icon: React.ReactNode, label: string, value: string, delta: string }) => (
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
