import { FormEvent, useState } from "react";
import { Alert } from "../components/common/Alert";
import { ColorField } from "../components/common/ColorField";
import { PageHeader } from "../components/common/PageHeader";
import { ThemePreview } from "../components/theme/ThemePreview";
import { useTheme } from "../hooks/useTheme";
import { ApiError } from "../types/api";
import { emptyTheme, type Theme, type ThemeWriteModel } from "../types/theme";
import { slugify } from "../utils/validation";

export const Themes = () => {
  const { themes, loading, error, save, remove } = useTheme();
  const [draft, setDraft] = useState<ThemeWriteModel>(emptyTheme());
  const [editingId, setEditingId] = useState<string | null>(null);
  const [formError, setFormError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const beginEdit = (theme: Theme) => {
    setEditingId(theme.id);
    setDraft({
      name: theme.name,
      slug: theme.slug,
      primaryColor: theme.primaryColor,
      secondaryColor: theme.secondaryColor,
      accentColor: theme.accentColor,
      backgroundColor: theme.backgroundColor,
      surfaceColor: theme.surfaceColor,
      textColor: theme.textColor,
      mutedTextColor: theme.mutedTextColor,
      borderColor: theme.borderColor,
      fontFamily: theme.fontFamily,
      borderRadius: theme.borderRadius,
      isDefault: theme.isDefault
    });
  };

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setFormError(null);
    try {
      await save(editingId, draft);
      setMessage("Theme saved.");
      setEditingId(null);
      setDraft(emptyTheme());
    } catch (caught) {
      setFormError(caught instanceof ApiError ? caught.message : "Unable to save theme.");
    }
  };

  return (
    <div>
      <PageHeader title="Themes" description="Reusable palettes inherited by product branding." />
      <Alert message={error} />
      <Alert kind="success" message={message} />
      <div className="editor-layout">
        <form className="panel stack" onSubmit={(event) => void onSubmit(event)}>
          <h2>{editingId ? "Edit theme" : "New theme"}</h2>
          <Alert message={formError} />
          <div className="form-grid">
            <label>
              Name
              <input
                value={draft.name}
                onChange={(event) =>
                  setDraft({
                    ...draft,
                    name: event.target.value,
                    slug: editingId ? draft.slug : slugify(event.target.value)
                  })
                }
              />
            </label>
            <label>
              Slug
              <input value={draft.slug} onChange={(event) => setDraft({ ...draft, slug: event.target.value })} />
            </label>
            <ColorField label="Primary" value={draft.primaryColor} onChange={(primaryColor) => setDraft({ ...draft, primaryColor })} />
            <ColorField label="Secondary" value={draft.secondaryColor} onChange={(secondaryColor) => setDraft({ ...draft, secondaryColor })} />
            <ColorField label="Accent" value={draft.accentColor} onChange={(accentColor) => setDraft({ ...draft, accentColor })} />
            <ColorField label="Background" value={draft.backgroundColor} onChange={(backgroundColor) => setDraft({ ...draft, backgroundColor })} />
            <ColorField label="Surface" value={draft.surfaceColor} onChange={(surfaceColor) => setDraft({ ...draft, surfaceColor })} />
            <ColorField label="Text" value={draft.textColor} onChange={(textColor) => setDraft({ ...draft, textColor })} />
            <ColorField label="Muted text" value={draft.mutedTextColor} onChange={(mutedTextColor) => setDraft({ ...draft, mutedTextColor })} />
            <ColorField label="Border" value={draft.borderColor} onChange={(borderColor) => setDraft({ ...draft, borderColor })} />
            <label>
              Font family
              <input value={draft.fontFamily} onChange={(event) => setDraft({ ...draft, fontFamily: event.target.value })} />
            </label>
            <label>
              Border radius
              <input value={draft.borderRadius} onChange={(event) => setDraft({ ...draft, borderRadius: event.target.value })} />
            </label>
          </div>
          <label>
            <input
              type="checkbox"
              checked={draft.isDefault}
              onChange={(event) => setDraft({ ...draft, isDefault: event.target.checked })}
            />{" "}
            Default theme
          </label>
          <button className="button" type="submit">
            Save theme
          </button>
        </form>
        <div className="stack">
          <ThemePreview value={draft} />
          <div className="table-wrap">
            {loading ? (
              <p className="empty">Loading themes...</p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Default</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {themes.map((theme) => (
                    <tr key={theme.id}>
                      <td>{theme.name}</td>
                      <td>{theme.isDefault ? "Yes" : "No"}</td>
                      <td className="actions">
                        <button className="button-secondary" type="button" onClick={() => beginEdit(theme)}>
                          Edit
                        </button>
                        <button className="button danger" type="button" onClick={() => void remove(theme.id)}>
                          Delete
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
