/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  darkMode : 'class',
  theme: {
    extend: {
      textUnderlineOffset: {
        6 : '6px',
        16: '16px',
      },
      animation : {
        'pulse-0.8' : 'pulse 0.8s linear infinite',
        'pulse-1.2' : 'pulse 1.2s linear infinite',
        'pulse-1.6' : 'pulse 1.6s linear infinite',
        'pulse-2' : 'pulse 2s linear infinite',
        'pulse-2.5' : 'pulse 2.5s linear infinite',
        'pulse-3' : 'pulse 3s linear infinite',
        'pulse-6' : 'pulse 6s linear infinite',
      }
    },
  },
  plugins: [],
};
