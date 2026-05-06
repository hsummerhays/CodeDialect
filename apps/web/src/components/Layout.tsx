import React from 'react';
import { Terminal, Code2, LayoutDashboard, Settings, User } from 'lucide-react';
import { Link, useLocation } from 'react-router-dom';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  const location = useLocation();

  return (
    <div className="flex h-screen bg-background overflow-hidden text-foreground">
      {/* Sidebar */}
      <aside className="w-64 border-r border-border flex flex-col glass-panel z-10">
        <Link to="/" className="p-8 flex items-center gap-3 group">
          <div className="bg-primary/20 p-2 rounded-xl group-hover:scale-110 transition-transform">
            <Terminal className="text-primary h-6 w-6" />
          </div>
          <span className="font-bold text-xl tracking-tight bg-clip-text text-transparent bg-gradient-to-r from-white to-gray-500">
            CodeDialect
          </span>
        </Link>
        
        <nav className="flex-1 px-4 space-y-2">
          <NavItem 
            to="/" 
            icon={<LayoutDashboard size={20} />} 
            label="Dashboard" 
            active={location.pathname === '/'} 
          />
          <NavItem
            to="/challenges"
            icon={<Code2 size={20} />}
            label="Challenges"
            active={location.pathname.startsWith('/challenge')}
          />
          <NavItem
            to="/settings"
            icon={<Settings size={20} />}
            label="Settings"
          />
        </nav>

        <div className="p-6 border-t border-border mt-auto">
          <div className="flex items-center gap-3 p-3 rounded-2xl hover:bg-white/[0.05] transition-all cursor-pointer border border-transparent hover:border-white/10 group">
            <div className="bg-gradient-to-br from-primary/40 to-primary/10 p-2 rounded-xl group-hover:rotate-12 transition-transform">
              <User size={20} className="text-primary-foreground" />
            </div>
            <div className="overflow-hidden">
              <p className="text-sm font-bold truncate">Senior Architect</p>
              <p className="text-[10px] text-muted-foreground uppercase tracking-widest">Master Tier</p>
            </div>
          </div>
        </div>
      </aside>

      {/* Main Content */}
      <main className="flex-1 overflow-y-auto relative">
        <div className="absolute top-0 left-0 w-full h-64 bg-gradient-to-b from-primary/5 to-transparent pointer-events-none" />
        <div className="p-8 relative z-0">
          {children}
        </div>
      </main>
    </div>
  );
};

const NavItem = ({ to, icon, label, active = false }: { to: string, icon: React.ReactNode, label: string, active?: boolean }) => (
  <Link 
    to={to}
    className={`flex items-center gap-3 px-4 py-3 rounded-2xl cursor-pointer transition-all border ${
      active 
        ? 'bg-primary/10 text-primary border-primary/20 shadow-[0_0_20px_rgba(var(--primary),0.1)]' 
        : 'text-muted-foreground border-transparent hover:bg-white/[0.05] hover:text-white'
    }`}
  >
    {icon}
    <span className="font-semibold text-sm">{label}</span>
  </Link>
);

export default Layout;
