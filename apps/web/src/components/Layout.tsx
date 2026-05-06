import React from 'react';
import { Terminal, Code2, LayoutDashboard, Settings, User } from 'lucide-react';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div className="flex h-screen bg-background overflow-hidden">
      {/* Sidebar */}
      <aside className="w-64 border-r border-card-border flex flex-col glass-panel">
        <div className="p-6 flex items-center gap-3">
          <Terminal className="text-primary h-8 w-8" />
          <span className="font-bold text-xl tracking-tight">CodeDialect</span>
        </div>
        
        <nav className="flex-1 px-4 py-4 space-y-2">
          <NavItem icon={<LayoutDashboard size={20} />} label="Dashboard" active />
          <NavItem icon={<Code2 size={20} />} label="Challenges" />
          <NavItem icon={<Settings size={20} />} label="Admin" />
        </nav>

        <div className="p-4 border-t border-card-border">
          <div className="flex items-center gap-3 p-2 rounded-lg hover:bg-secondary transition-colors cursor-pointer">
            <div className="bg-primary/20 p-2 rounded-full">
              <User size={20} className="text-primary" />
            </div>
            <div>
              <p className="text-sm font-medium">Developer User</p>
              <p className="text-xs text-gray-400">View Profile</p>
            </div>
          </div>
        </div>
      </aside>

      {/* Main Content */}
      <main className="flex-1 overflow-y-auto p-8">
        {children}
      </main>
    </div>
  );
};

const NavItem = ({ icon, label, active = false }: { icon: React.ReactNode, label: string, active?: boolean }) => (
  <div className={`flex items-center gap-3 px-4 py-3 rounded-xl cursor-pointer transition-all ${active ? 'bg-primary/10 text-primary border border-primary/20' : 'text-gray-400 hover:bg-secondary hover:text-white'}`}>
    {icon}
    <span className="font-medium">{label}</span>
  </div>
);

export default Layout;
