import React from 'react';
import { Play, Share2, Star } from 'lucide-react';

interface ChallengeCardProps {
  title: string;
  description: string;
  difficulty: 'Beginner' | 'Intermediate' | 'Advanced' | 'Expert';
  category: string;
  tags: string[];
}

const ChallengeCard: React.FC<ChallengeCardProps> = ({ title, description, difficulty, category, tags }) => {
  const diffColors = {
    Beginner: 'text-green-400 bg-green-400/10 border-green-400/20',
    Intermediate: 'text-yellow-400 bg-yellow-400/10 border-yellow-400/20',
    Advanced: 'text-orange-400 bg-orange-400/10 border-orange-400/20',
    Expert: 'text-red-400 bg-red-400/10 border-red-400/20',
  };

  return (
    <div className="glass-panel p-6 rounded-2xl hover:border-primary/50 transition-all group">
      <div className="flex justify-between items-start mb-4">
        <div>
          <span className={`text-xs font-bold px-2 py-1 rounded border ${diffColors[difficulty]}`}>
            {difficulty.toUpperCase()}
          </span>
          <h3 className="text-xl font-bold mt-3 group-hover:text-primary transition-colors">{title}</h3>
        </div>
        <button className="text-gray-400 hover:text-white transition-colors">
          <Star size={20} />
        </button>
      </div>
      
      <p className="text-gray-400 text-sm mb-6 line-clamp-2">{description}</p>
      
      <div className="flex flex-wrap gap-2 mb-6">
        {tags.map(tag => (
          <span key={tag} className="text-[10px] uppercase font-bold tracking-wider text-gray-500 border border-card-border px-2 py-1 rounded-md">
            {tag}
          </span>
        ))}
      </div>

      <div className="flex items-center justify-between pt-4 border-t border-card-border">
        <span className="text-xs text-gray-500 font-medium">{category}</span>
        <div className="flex gap-2">
          <button className="p-2 rounded-lg hover:bg-secondary text-gray-400 transition-colors">
            <Share2 size={18} />
          </button>
          <button className="flex items-center gap-2 bg-primary hover:bg-primary-hover text-white px-4 py-2 rounded-xl text-sm font-bold transition-all transform hover:scale-105 active:scale-95">
            <Play size={16} /> Solve
          </button>
        </div>
      </div>
    </div>
  );
};

export default ChallengeCard;
