/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  darkMode: 'class',
  theme: {
    extend: {
      colors: {
        background: "#0a0a0a",
        foreground: "#ededed",
        primary: {
          DEFAULT: "#3b82f6",
          hover: "#2563eb",
        },
        secondary: {
          DEFAULT: "#1f2937",
          hover: "#374151",
        },
        card: {
          DEFAULT: "#171717",
          border: "#262626",
        }
      },
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
        mono: ['JetBrains Mono', 'monospace'],
      },
    },
  },
  plugins: [],
}
