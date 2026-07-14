export type WidgetType = 'Text' | 'LineChart' | 'BarChart';

export type ChartPoint = {
  order: number;
  label: string;
  value: number;
};

export type Widget = {
  id: string;
  type: WidgetType;
  position: number;
  text?: string | null;
  points?: ChartPoint[] | null;
  createdAt: string;
  updatedAt?: string | null;
};

export type WidgetStatus = 'idle' | 'loading' | 'error';

export const widgetTypeLabel: Record<WidgetType, string> = {
  Text: 'Text',
  LineChart: 'Line chart',
  BarChart: 'Bar chart',
};
