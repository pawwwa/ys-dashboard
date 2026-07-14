import { useState } from 'react';

type Props = {
  text: string;
  onSave: (text: string) => Promise<void>;
  isSaving?: boolean;
};

export function TextWidget({ text, onSave, isSaving = false }: Props) {
  const [isEditing, setIsEditing] = useState(false);
  const [draft, setDraft] = useState(text);

  const handleEdit = () => {
    setDraft(text);
    setIsEditing(true);
  };

  const handleSave = async () => {
    try {
      await onSave(draft);
      setIsEditing(false);
    } catch {
      // Stay in edit mode; parent shows the error.
    }
  };

  const handleCancel = () => {
    setDraft(text);
    setIsEditing(false);
  };

  if (isEditing) {
    return (
      <div className="text-widget">
        <textarea
          className="text-widget__input"
          value={draft}
          onChange={(e) => setDraft(e.target.value)}
          rows={5}
          autoFocus
          disabled={isSaving}
        />
        <div className="text-widget__actions">
          <button
            type="button"
            className="btn btn--primary"
            onClick={() => void handleSave()}
            disabled={isSaving}
          >
            {isSaving ? 'Saving...' : 'Save'}
          </button>
          <button
            type="button"
            className="btn btn--ghost"
            onClick={handleCancel}
            disabled={isSaving}
          >
            Cancel
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="text-widget">
      <p className="text-widget__content">{text}</p>
      <button type="button" className="btn btn--secondary" onClick={handleEdit}>
        Edit
      </button>
    </div>
  );
}
