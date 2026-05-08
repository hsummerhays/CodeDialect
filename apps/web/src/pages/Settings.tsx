import { motion } from 'framer-motion';
import { 
  User, 
  Bell, 
  Shield, 
  Code2, 
  Palette, 
  Globe, 
  Save,
  CheckCircle2
} from 'lucide-react';
import { useState } from 'react';

const Settings = () => {
  const [activeTab, setActiveTab] = useState('profile');
  const [isSaving, setIsSaving] = useState(false);
  const [saved, setSaved] = useState(false);

  const tabs = [
    { id: 'profile', label: 'Profile', icon: <User size={18} /> },
    { id: 'appearance', label: 'Appearance', icon: <Palette size={18} /> },
    { id: 'notifications', label: 'Notifications', icon: <Bell size={18} /> },
    { id: 'security', label: 'Security', icon: <Shield size={18} /> },
    { id: 'editor', label: 'Editor Preferences', icon: <Code2 size={18} /> },
    { id: 'language', label: 'Language & Region', icon: <Globe size={18} /> },
  ];

  const handleSave = () => {
    setIsSaving(true);
    setTimeout(() => {
      setIsSaving(false);
      setSaved(true);
      setTimeout(() => setSaved(false), 3000);
    }, 1000);
  };

  return (
    <div className="max-w-5xl mx-auto space-y-8">
      <header className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold tracking-tight">Settings</h1>
          <p className="text-gray-400 mt-1">Manage your account and platform preferences.</p>
        </div>
        <button 
          onClick={handleSave}
          disabled={isSaving}
          className="flex items-center gap-2 px-6 py-2.5 bg-primary text-primary-foreground rounded-xl font-bold hover:opacity-90 transition-all disabled:opacity-50"
        >
          {isSaving ? (
            <motion.div
              animate={{ rotate: 360 }}
              transition={{ repeat: Infinity, duration: 1, ease: "linear" }}
            >
              <Save size={20} />
            </motion.div>
          ) : saved ? (
            <CheckCircle2 size={20} />
          ) : (
            <Save size={20} />
          )}
          {saved ? 'Settings Saved' : 'Save Changes'}
        </button>
      </header>

      <div className="flex flex-col md:flex-row gap-8">
        {/* Sidebar Tabs */}
        <aside className="w-full md:w-64 space-y-1">
          {tabs.map((tab) => (
            <button
              key={tab.id}
              onClick={() => setActiveTab(tab.id)}
              className={`w-full flex items-center gap-3 px-4 py-3 rounded-xl transition-all border ${
                activeTab === tab.id
                  ? 'bg-primary/10 text-primary border-primary/20 font-bold shadow-[0_0_20px_rgba(var(--primary),0.1)]'
                  : 'text-gray-400 border-transparent hover:bg-white/5 hover:text-white'
              }`}
            >
              {tab.icon}
              <span className="text-sm">{tab.label}</span>
            </button>
          ))}
        </aside>

        {/* Content Area */}
        <motion.div 
          key={activeTab}
          initial={{ opacity: 0, x: 20 }}
          animate={{ opacity: 1, x: 0 }}
          className="flex-1 glass-panel p-8 rounded-3xl border border-white/5 space-y-8"
        >
          {activeTab === 'profile' && (
            <div className="space-y-6">
              <div className="flex items-center gap-6">
                <div className="h-24 w-24 rounded-full bg-gradient-to-br from-primary to-blue-600 flex items-center justify-center text-3xl font-bold border-4 border-background shadow-xl">
                  SA
                </div>
                <div>
                  <h3 className="text-xl font-bold">Senior Architect</h3>
                  <p className="text-sm text-gray-400">Master Tier • Member since May 2026</p>
                  <button className="text-primary text-sm font-bold mt-2 hover:underline">Change avatar</button>
                </div>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <InputGroup label="Display Name" placeholder="Senior Architect" />
                <InputGroup label="Email Address" placeholder="architect@codedialect.io" />
                <div className="md:col-span-2">
                  <label className="block text-sm font-bold text-gray-400 mb-2 uppercase tracking-widest text-[10px]">Biography</label>
                  <textarea 
                    className="w-full bg-background border border-white/5 rounded-xl p-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/50 h-32 transition-all"
                    placeholder="Tell us about your coding journey..."
                  />
                </div>
              </div>
            </div>
          )}

          {activeTab === 'appearance' && (
            <div className="space-y-8">
              <div>
                <h3 className="text-lg font-bold mb-4">Color Theme</h3>
                <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                  <ThemeOption name="Midnight" active color="bg-slate-950" />
                  <ThemeOption name="Deep Ocean" color="bg-blue-950" />
                  <ThemeOption name="Emerald" color="bg-green-950" />
                  <ThemeOption name="Nebula" color="bg-purple-950" />
                </div>
              </div>

              <div>
                <h3 className="text-lg font-bold mb-4">Glassmorphism Intensity</h3>
                <input type="range" className="w-full accent-primary" />
                <div className="flex justify-between text-xs text-gray-500 mt-2">
                  <span>Subtle</span>
                  <span>Extreme</span>
                </div>
              </div>
            </div>
          )}

          {activeTab === 'editor' && (
            <div className="space-y-6">
              <ToggleGroup 
                label="Monaco Font" 
                options={['Fira Code', 'JetBrains Mono', 'Source Code Pro']} 
                defaultValue="Fira Code" 
              />
              <ToggleGroup 
                label="Font Size" 
                options={['12px', '14px', '16px', '18px']} 
                defaultValue="14px" 
              />
              <div className="flex items-center justify-between p-4 bg-white/5 rounded-2xl">
                <div>
                  <p className="font-bold">Ligatures</p>
                  <p className="text-xs text-gray-400">Enable programming ligatures in the editor</p>
                </div>
                <input type="checkbox" defaultChecked className="h-5 w-5 accent-primary" />
              </div>
            </div>
          )}

          {activeTab !== 'profile' && activeTab !== 'appearance' && activeTab !== 'editor' && (
            <div className="flex flex-col items-center justify-center py-20 text-center space-y-4">
              <div className="p-4 bg-white/5 rounded-full">
                <Code2 size={48} className="text-gray-600" />
              </div>
              <div>
                <h3 className="text-xl font-bold italic">Engineering in Progress</h3>
                <p className="text-gray-400 max-w-xs">This settings module is being optimized for the next deployment.</p>
              </div>
            </div>
          )}
        </motion.div>
      </div>
    </div>
  );
};

const InputGroup = ({ label, placeholder }: { label: string; placeholder: string }) => (
  <div className="space-y-2">
    <label className="block text-sm font-bold text-gray-400 uppercase tracking-widest text-[10px]">{label}</label>
    <input 
      type="text" 
      className="w-full bg-background border border-white/5 rounded-xl p-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/50 transition-all"
      placeholder={placeholder}
      defaultValue={placeholder}
    />
  </div>
);

const ThemeOption = ({ name, color, active = false }: { name: string; color: string; active?: boolean }) => (
  <div className={`p-4 rounded-2xl border ${active ? 'border-primary ring-2 ring-primary/20' : 'border-white/5'} cursor-pointer hover:border-white/20 transition-all`}>
    <div className={`w-full aspect-video rounded-lg ${color} mb-3`} />
    <p className="text-xs font-bold text-center">{name}</p>
  </div>
);

const ToggleGroup = ({ label, options, defaultValue }: { label: string; options: string[]; defaultValue: string }) => (
  <div className="space-y-3">
    <label className="block text-sm font-bold text-gray-400 uppercase tracking-widest text-[10px]">{label}</label>
    <div className="flex flex-wrap gap-2">
      {options.map(opt => (
        <button 
          key={opt}
          className={`px-4 py-2 rounded-lg text-xs font-bold transition-all border ${
            opt === defaultValue 
              ? 'bg-primary/20 border-primary text-primary' 
              : 'border-white/5 text-gray-400 hover:border-white/20'
          }`}
        >
          {opt}
        </button>
      ))}
    </div>
  </div>
);

export default Settings;
