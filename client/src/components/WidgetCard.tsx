import type { Widget, WidgetStatus } from '../types/widget';
import { widgetTypeLabel } from '../types/widget';
import { BarChartWidget } from './widgets/BarChartWidget';
import { LineChartWidget } from './widgets/LineChartWidget';
import { TextWidget } from './widgets/TextWidget';

type Props = {
  widget: Widget;
  status?: WidgetStatus;
  errorMessage?: string;
  onDelete: (id: string) => void;
  onTextSave: (id: string, text: string) => Promise<void>;
};

export function WidgetCard({
  widget,
  status = 'idle',
  errorMessage,
  onDelete,
  onTextSave,
}: Props) {
  const isLoading = status === 'loading';
  const isError = status === 'error';
  const title = widgetTypeLabel[widget.type];

  return (
    <article className={`widget-card ${isLoading ? 'widget-card--loading' : ''}`}>
      <header className="widget-card__header">
        <h3 className="widget-card__title">{title}</h3>
        <button
          type="button"
          className="btn btn--danger btn--icon"
          onClick={() => onDelete(widget.id)}
          aria-label="Delete widget"
          disabled={isLoading}
        >
          ×
        </button>
      </header>

      <div className="widget-card__body">
        {isLoading && widget.type !== 'Text' && (
          <div className="widget-card__overlay">
            <div className="spinner" />
            <span>Loading...</span>
          </div>
        )}

        {isError && (
          <div className="widget-card__error">
            <strong>Something went wrong</strong>
            <p>{errorMessage ?? 'Failed to update widget.'}</p>
          </div>
        )}

        {widget.type === 'LineChart' && widget.points && (
          <LineChartWidget data={widget.points} />
        )}

        {widget.type === 'BarChart' && widget.points && (
          <BarChartWidget data={widget.points} />
        )}

        {widget.type === 'Text' && (
          <TextWidget
            text={widget.text ?? ''}
            onSave={(text) => onTextSave(widget.id, text)}
            isSaving={isLoading}
          />
        )}
      </div>
    </article>
  );
}
