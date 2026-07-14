# Client

React + TypeScript + Vite + Recharts frontend for the widget dashboard.

## Setup

```bash
npm install
npm run dev
```

Dev server: http://localhost:5173  
By default `/api` is proxied to `http://localhost:5154`.

## Deploy

Set the API base URL:

```bash
VITE_API_URL=https://your-api.example.com
```

Leave unset for local proxy.

## Scripts

```bash
npm run build    # typecheck + production build
npm run preview  # preview production build
```
