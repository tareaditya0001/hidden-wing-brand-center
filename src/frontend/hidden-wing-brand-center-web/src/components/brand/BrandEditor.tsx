import { ColorField } from "../common/ColorField";
import type { ProductBranding } from "../../types/product";
import type { Theme } from "../../types/theme";

interface BrandEditorProps {
  value: ProductBranding;
  themes: Theme[];
  onChange: (value: ProductBranding) => void;
}

export const BrandEditor = ({ value, themes, onChange }: BrandEditorProps) => {
  const update = (patch: Partial<ProductBranding>) => {
    onChange({ ...value, ...patch });
  };

  return (
    <div className="stack">
      <section className="panel stack">
        <h2 className="section-title">Identity</h2>
        <div className="form-grid">
          <label>
            Product name
            <input value={value.displayName ?? ""} onChange={(event) => update({ displayName: event.target.value })} />
          </label>
          <label>
            Short name
            <input value={value.shortName ?? ""} onChange={(event) => update({ shortName: event.target.value })} />
          </label>
          <label>
            Tagline
            <input value={value.tagline ?? ""} onChange={(event) => update({ tagline: event.target.value })} />
          </label>
          <label>
            Theme
            <select value={value.themeId ?? ""} onChange={(event) => update({ themeId: event.target.value || null })}>
              <option value="">Default theme</option>
              {themes.map((theme) => (
                <option key={theme.id} value={theme.id}>
                  {theme.name}
                </option>
              ))}
            </select>
          </label>
        </div>
      </section>

      <section className="panel stack">
        <h2 className="section-title">Logos</h2>
        <div className="form-grid">
          <label>
            Primary logo
            <input value={value.logoUrl ?? ""} onChange={(event) => update({ logoUrl: event.target.value })} />
          </label>
          <label>
            Dark logo
            <input value={value.logoDarkUrl ?? ""} onChange={(event) => update({ logoDarkUrl: event.target.value })} />
          </label>
          <label>
            Light logo
            <input value={value.logoLightUrl ?? ""} onChange={(event) => update({ logoLightUrl: event.target.value })} />
          </label>
          <label>
            Icon
            <input value={value.iconUrl ?? ""} onChange={(event) => update({ iconUrl: event.target.value })} />
          </label>
          <label>
            Favicon
            <input value={value.faviconUrl ?? ""} onChange={(event) => update({ faviconUrl: event.target.value })} />
          </label>
        </div>
      </section>

      <section className="panel stack">
        <h2 className="section-title">Colors</h2>
        <div className="form-grid">
          <ColorField label="Primary" value={value.primaryColor ?? ""} onChange={(primaryColor) => update({ primaryColor })} />
          <ColorField label="Secondary" value={value.secondaryColor ?? ""} onChange={(secondaryColor) => update({ secondaryColor })} />
          <ColorField label="Accent" value={value.accentColor ?? ""} onChange={(accentColor) => update({ accentColor })} />
          <ColorField label="Background" value={value.backgroundColor ?? ""} onChange={(backgroundColor) => update({ backgroundColor })} />
          <ColorField label="Surface" value={value.surfaceColor ?? ""} onChange={(surfaceColor) => update({ surfaceColor })} />
          <ColorField label="Text" value={value.textColor ?? ""} onChange={(textColor) => update({ textColor })} />
          <ColorField label="Muted text" value={value.mutedTextColor ?? ""} onChange={(mutedTextColor) => update({ mutedTextColor })} />
          <ColorField label="Border" value={value.borderColor ?? ""} onChange={(borderColor) => update({ borderColor })} />
        </div>
      </section>

      <section className="panel stack">
        <h2 className="section-title">Typography and links</h2>
        <div className="form-grid">
          <label>
            Font family
            <input value={value.fontFamily ?? ""} onChange={(event) => update({ fontFamily: event.target.value })} />
          </label>
          <label>
            Website
            <input value={value.websiteUrl ?? ""} onChange={(event) => update({ websiteUrl: event.target.value })} />
          </label>
          <label>
            Support email
            <input value={value.supportEmail ?? ""} onChange={(event) => update({ supportEmail: event.target.value })} />
          </label>
          <label>
            Privacy URL
            <input value={value.privacyUrl ?? ""} onChange={(event) => update({ privacyUrl: event.target.value })} />
          </label>
          <label>
            Terms URL
            <input value={value.termsUrl ?? ""} onChange={(event) => update({ termsUrl: event.target.value })} />
          </label>
          <label>
            Enabled
            <select value={value.isEnabled ? "true" : "false"} onChange={(event) => update({ isEnabled: event.target.value === "true" })}>
              <option value="true">Enabled</option>
              <option value="false">Disabled</option>
            </select>
          </label>
        </div>
      </section>
    </div>
  );
};
