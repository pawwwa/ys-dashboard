import type { WidgetType } from '../types/widget';

type Props = {
  onAdd: (type: WidgetType) => void;
  disabled?: boolean;
};

const buttons: { type: WidgetType; label: string }[] = [
  { type: 'LineChart', label: '+ Line chart' },
  { type: 'BarChart', label: '+ Bar chart' },
  { type: 'Text', label: '+ Text' },
];

export function AddWidgetButtons({ onAdd, disabled }: Props) {
  return (
    <div className="add-buttons">
      {buttons.map(({ type, label }) => (
        <button
          key={type}
          type="button"
          className="btn btn--primary"
          onClick={() => onAdd(type)}
          disabled={disabled}
        >
          {label}
        </button>
      ))}
    </div>
  );
}
