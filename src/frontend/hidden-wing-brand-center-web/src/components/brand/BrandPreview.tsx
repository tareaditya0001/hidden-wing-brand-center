import { fallbackColor } from "../../utils/colors";
import { resolveAssetUrl } from "../../utils/urls";

export interface BrandPreviewModel {
  productName: string;
  shortName?: string | null;
  tagline?: string | null;
  logoUrl?: string | null;
  primaryColor?: string | null;
  secondaryColor?: string | null;
  accentColor?: string | null;
  backgroundColor?: string | null;
  surfaceColor?: string | null;
  textColor?: string | null;
  mutedTextColor?: string | null;
  borderColor?: string | null;
  fontFamily?: string | null;
}

interface BrandPreviewProps {
  value: BrandPreviewModel;
}

export const BrandPreview = ({ value }: BrandPreviewProps) => {
  const primary = fallbackColor(value.primaryColor, "#1C3353");
  const accent = fallbackColor(value.accentColor, "#C6A15B");
  const background = fallbackColor(value.backgroundColor, "#F4EFE6");
  const surface = fallbackColor(value.surfaceColor, "#FFFFFF");
  const text = fallbackColor(value.textColor, "#1A1F29");
  const muted = fallbackColor(value.mutedTextColor, "#5C6B7A");
  const border = fallbackColor(value.borderColor, "#D9D2C5");
  const fontFamily = value.fontFamily || "Inter, system-ui, sans-serif";

  return (
    <div
      className="preview-frame"
      style={{
        background,
        color: text,
        fontFamily,
        ["--preview-border" as string]: border,
        ["--preview-surface" as string]: surface
      }}
    >
      <div className="preview-bar" style={{ background: surface }}>
        <div className="split">
          <img src={resolveAssetUrl(value.logoUrl)} alt={value.productName} style={{ height: 28 }} />
          <strong>{value.shortName || value.productName}</strong>
        </div>
        <div className="actions">
          <span className="muted" style={{ color: muted }}>Overview</span>
          <span className="muted" style={{ color: muted }}>Catalog</span>
        </div>
      </div>
      <div className="preview-body stack">
        <div>
          <p className="muted" style={{ color: muted, marginTop: 0 }}>
            {value.tagline || "Live branding preview"}
          </p>
          <h2 style={{ marginTop: 0 }}>{value.productName}</h2>
        </div>
        <div className="actions">
          <button className="button" style={{ background: primary }}>
            Primary action
          </button>
          <button className="button" style={{ background: accent, color: "#1A1F29" }}>
            Accent
          </button>
        </div>
        <div className="preview-card">
          <strong>Card surface</strong>
          <p className="muted" style={{ color: muted }}>
            Buttons, cards, inputs and typography update from local editor state before save.
          </p>
          <input placeholder="Search products" />
        </div>
      </div>
    </div>
  );
};
