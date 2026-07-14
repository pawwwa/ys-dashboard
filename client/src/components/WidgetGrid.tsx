import type { Widget, WidgetStatus } from '../types/widget';
import { WidgetCard } from './WidgetCard';

type Props = {
  widgets: Widget[];
  getStatus?: (id: string) => WidgetStatus;
  getError?: (id: string) => string | undefined;
  onDelete: (id: string) => void;
  onTextSave: (id: string, text: string) => Promise<void>;
};

export function WidgetGrid({
  widgets,
  getStatus,
  getError,
  onDelete,
  onTextSave,
}: Props) {
  const sorted = [...widgets].sort((a, b) => a.position - b.position);

  if (sorted.length === 0) {
    return (
      <div className="empty-state">
        <p>No widgets yet.</p>
        <p className="empty-state__hint">Use the buttons above to add your first widget.</p>
      </div>
    );
  }

  return (
    <div className="widget-grid">
      {sorted.map((widget) => (
        <WidgetCard
          key={widget.id}
          widget={widget}
          status={getStatus?.(widget.id) ?? 'idle'}
          errorMessage={getError?.(widget.id)}
          onDelete={onDelete}
          onTextSave={onTextSave}
        />
      ))}
    </div>
  );
}
