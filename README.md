# YouScan Dashboard

Full-stack widget dashboard: **ASP.NET Core** backend + **React 18/19 + TypeScript** frontend.


## Project structure

```text
ys-dashboard/
  backend/YouScanDashboard.Api/   # .NET API
  client/                         # Vite + React + TypeScript
```

## API

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/widgets` | List all widgets |
| `GET` | `/api/widgets/{id}` | Get one widget |
| `POST` | `/api/widgets` | Create (`{ "type": "Text" \| "LineChart" \| "BarChart", "text"?: string }`) |
| `PUT` | `/api/widgets/{id}/text` | Update text (`{ "text": "..." }`) |
| `DELETE` | `/api/widgets/{id}` | Delete widget |

Invalid input returns `400`. Missing widgets return `404`.

