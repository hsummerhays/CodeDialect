# CodeDialect — Web App

React 19 + TypeScript 6 frontend for the CodeDialect platform.

## Stack

| | |
|---|---|
| Framework | React 19 |
| Language | TypeScript 6 |
| Build | Vite 8 |
| Styling | Tailwind CSS v4 |
| Editor | `@monaco-editor/react` (Monaco / VS Code engine) |
| Data fetching | TanStack Query v5 + Axios |
| Routing | React Router v7 |
| Animation | Framer Motion |
| State | Zustand |

---

## Development

```bash
# From the repo root (starts API + web together)
npm run dev

# Or from this directory alone
npm run dev
```

The app runs at `http://localhost:5173`. Vite proxies `/api/*` to the API at `http://localhost:5187`, so no CORS configuration is needed in development.

### Build

```bash
npm run build        # type-check + Vite production build → dist/
npm run preview      # serve the production build locally
```

### Lint

```bash
npm run lint
```

---

## Structure

```
src/
├── lib/
│   └── api.ts              # Axios instance, typed API client, shared DTOs
├── components/
│   ├── ComparisonViewer.tsx # Side-by-side / diff Monaco editor panel
│   ├── ChallengeCard.tsx    # Card shown in the challenge grid
│   ├── Layout.tsx           # Sidebar navigation shell
│   └── ErrorBoundary.tsx    # React error boundary
├── pages/
│   └── ChallengeDetail.tsx  # Challenge detail + dialect selector
├── App.tsx                  # Routes, QueryClientProvider, Home page
└── main.tsx                 # Entry point
```

---

## API Client (`src/lib/api.ts`)

All backend communication goes through `challengesApi`. Types mirror the backend DTOs exactly.

```ts
challengesApi.getAll({ page?, pageSize?, difficulty? })
  // → PaginatedResult<ChallengeDto>

challengesApi.getById(id: string)
  // → ChallengeDetailsDto

challengesApi.submit(id: string, { dialectId, code })
  // → SubmissionResultDto   (requires auth — returns 401 until auth UI is built)
```

A 401 response interceptor logs a warning and is wired for a future redirect to `/login`.

---

## Key Components

### `ComparisonViewer`

Accepts `leftLanguage` / `rightLanguage` props for per-side Monaco syntax highlighting. Scroll sync between editors uses an `isSyncing` guard ref to prevent feedback loops. Toggle between side-by-side and diff views.

Props:
```ts
leftCode, rightCode
leftTitle, rightTitle
leftLanguage, rightLanguage   // Monaco language identifiers (e.g. 'csharp', 'java')
displayLanguage               // Badge shown in the header
challengeTitle, description
leftReferenceSolution?, rightReferenceSolution?
leftFeatures?, rightFeatures? // Syntax feature tags
```

### `ChallengeDetail`

Fetches challenge data via TanStack Query. When a challenge has more than two implementations, renders dialect selectors for left and right panels. Derives Monaco language identifiers from the `languageName` field using a local lookup map.

---

## Environment Variables

| Variable | Description |
|---|---|
| `VITE_API_URL` | API base URL for production builds. Omit in development (Vite proxy handles it). |

Create a `.env.local` file for local overrides (not committed).

---

## Not Yet Implemented

- Login / register pages (JWT backend is ready, no auth UI)
- Search and filter controls (UI rendered but not wired)
- Sorting (UI rendered but not wired)
- Run Benchmark button (placeholder — execution engine not built)
- Share / Copy Link button (placeholder)
- Settings page
- Dashboard stats (hardcoded zeros)
