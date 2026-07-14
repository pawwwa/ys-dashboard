import type { Widget, WidgetType } from '../types/widget';

const API_BASE = (import.meta.env.VITE_API_URL as string | undefined)?.replace(/\/$/, '') ?? '';

export class ApiError extends Error {
  status: number;

  constructor(message: string, status: number) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
  }
}

export function getErrorMessage(error: unknown, fallback: string): string {
  return error instanceof ApiError ? error.message : fallback;
}

async function parseError(response: Response): Promise<string> {
  try {
    const body = (await response.json()) as {
      title?: string;
      detail?: string;
      errors?: Record<string, string[]>;
    };

    if (body.errors) {
      return Object.values(body.errors).flat().join(' ');
    }

    return body.detail ?? body.title ?? `Request failed (${response.status})`;
  } catch {
    return `Request failed (${response.status})`;
  }
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE}${path}`, {
    ...init,
    headers: {
      'Content-Type': 'application/json',
      ...init?.headers,
    },
  });

  if (!response.ok) {
    throw new ApiError(await parseError(response), response.status);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

export const widgetsApi = {
  list: () => request<Widget[]>('/api/widgets'),

  create: (type: WidgetType, text?: string) =>
    request<Widget>('/api/widgets', {
      method: 'POST',
      body: JSON.stringify({ type, text }),
    }),

  updateText: (id: string, text: string) =>
    request<Widget>(`/api/widgets/${id}/text`, {
      method: 'PUT',
      body: JSON.stringify({ text }),
    }),

  remove: (id: string) =>
    request<void>(`/api/widgets/${id}`, {
      method: 'DELETE',
    }),
};
