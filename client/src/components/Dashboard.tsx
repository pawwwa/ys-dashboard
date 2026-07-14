import { useCallback, useEffect, useState } from 'react';
import { getErrorMessage, widgetsApi } from '../api/widgetsApi';
import type { Widget, WidgetStatus, WidgetType } from '../types/widget';
import { AddWidgetButtons } from './AddWidgetButtons';
import { WidgetGrid } from './WidgetGrid';

export function Dashboard() {
  const [widgets, setWidgets] = useState<Widget[]>([]);
  const [pageLoading, setPageLoading] = useState(true);
  const [pageError, setPageError] = useState<string | null>(null);
  const [statuses, setStatuses] = useState<Record<string, WidgetStatus>>({});
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [creating, setCreating] = useState(false);

  const setWidgetStatus = useCallback((id: string, status: WidgetStatus, message?: string) => {
    setStatuses((prev) => ({ ...prev, [id]: status }));
    setErrors((prev) => {
      const next = { ...prev };
      if (status === 'error' && message) {
        next[id] = message;
      } else {
        delete next[id];
      }
      return next;
    });
  }, []);

  const loadWidgets = useCallback(async () => {
    setPageLoading(true);
    setPageError(null);

    try {
      setWidgets(await widgetsApi.list());
    } catch (error) {
      setPageError(getErrorMessage(error, 'Failed to load widgets.'));
    } finally {
      setPageLoading(false);
    }
  }, []);

  useEffect(() => {
    void loadWidgets();
  }, [loadWidgets]);

  const handleAdd = async (type: WidgetType) => {
    setCreating(true);
    setPageError(null);

    try {
      const created = await widgetsApi.create(type);
      setWidgets((prev) => [...prev, created]);
    } catch (error) {
      setPageError(getErrorMessage(error, 'Failed to create widget.'));
    } finally {
      setCreating(false);
    }
  };

  const handleDelete = async (id: string) => {
    setWidgetStatus(id, 'loading');

    try {
      await widgetsApi.remove(id);
      setWidgets((prev) =>
        prev
          .filter((w) => w.id !== id)
          .map((w, index) => ({ ...w, position: index })),
      );
      setStatuses((prev) => {
        const next = { ...prev };
        delete next[id];
        return next;
      });
      setErrors((prev) => {
        const next = { ...prev };
        delete next[id];
        return next;
      });
    } catch (error) {
      setWidgetStatus(id, 'error', getErrorMessage(error, 'Failed to delete widget.'));
    }
  };

  const handleTextSave = async (id: string, text: string) => {
    setWidgetStatus(id, 'loading');

    try {
      const updated = await widgetsApi.updateText(id, text);
      setWidgets((prev) => prev.map((w) => (w.id === id ? updated : w)));
      setWidgetStatus(id, 'idle');
    } catch (error) {
      const message = getErrorMessage(error, 'Failed to save text.');
      setWidgetStatus(id, 'error', message);
      throw error;
    }
  };

  return (
    <div className="dashboard">
      <header className="dashboard__header">
        <div>
          <h1>Widget Dashboard</h1>
          <p className="dashboard__subtitle">
            Add line charts, bar charts, and editable text widgets
          </p>
        </div>
        <AddWidgetButtons onAdd={(type) => void handleAdd(type)} disabled={creating || pageLoading} />
      </header>

      {pageError && (
        <div className="page-banner page-banner--error" role="alert">
          <span>{pageError}</span>
          <button type="button" className="btn btn--ghost" onClick={() => void loadWidgets()}>
            Retry
          </button>
        </div>
      )}

      {pageLoading ? (
        <div className="page-loading">
          <div className="spinner" />
          <span>Loading dashboard...</span>
        </div>
      ) : (
        <WidgetGrid
          widgets={widgets}
          getStatus={(id) => statuses[id] ?? 'idle'}
          getError={(id) => errors[id]}
          onDelete={(id) => void handleDelete(id)}
          onTextSave={handleTextSave}
        />
      )}
    </div>
  );
}
