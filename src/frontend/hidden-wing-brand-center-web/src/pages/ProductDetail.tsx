import { useMemo, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { environment } from "../config/environment";
import { Alert } from "../components/common/Alert";
import { BrandEditor } from "../components/brand/BrandEditor";
import { BrandPreview } from "../components/brand/BrandPreview";
import { PageHeader } from "../components/common/PageHeader";
import { useProductDetail } from "../hooks/useProduct";
import { useTheme } from "../hooks/useTheme";
import { ApiError } from "../types/api";
import type { ProductBranding, ProductDetailTab } from "../types/product";

const tabs: { id: ProductDetailTab; label: string }[] = [
  { id: "overview", label: "Overview" },
  { id: "branding", label: "Branding" },
  { id: "theme", label: "Theme" },
  { id: "assets", label: "Assets" },
  { id: "integration", label: "Integration" }
];

export const ProductDetail = () => {
  const { id } = useParams();
  const { product, branding, loading, error, saveBranding } = useProductDetail(id);
  const { themes } = useTheme();
  const [draft, setDraft] = useState<ProductBranding | null>(null);
  const [tab, setTab] = useState<ProductDetailTab>("branding");
  const [message, setMessage] = useState<string | null>(null);
  const [formError, setFormError] = useState<string | null>(null);

  const editorValue = draft ?? branding;
  const selectedTheme = useMemo(
    () => themes.find((theme) => theme.id === editorValue?.themeId) ?? themes.find((theme) => theme.isDefault),
    [themes, editorValue?.themeId]
  );

  if (loading || !product || !editorValue) {
    return <p className="empty">{error ?? "Loading product..."}</p>;
  }

  const onSave = async () => {
    setFormError(null);
    try {
      await saveBranding(editorValue);
      setMessage("Product branding saved.");
    } catch (caught) {
      setFormError(caught instanceof ApiError ? caught.message : "Unable to save branding.");
    }
  };

  return (
    <div>
      <PageHeader
        title={product.name}
        description={`${product.slug} · inherits from ${product.brandName}`}
        actions={
          <Link className="button-secondary" to="/products">
            Back to products
          </Link>
        }
      />
      <Alert message={error} />
      <Alert kind="success" message={message} />
      <div className="tabs">
        {tabs.map((item) => (
          <button key={item.id} className={tab === item.id ? "tab active" : "tab"} type="button" onClick={() => setTab(item.id)}>
            {item.label}
          </button>
        ))}
      </div>

      {tab === "overview" && (
        <div className="panel stack">
          <p>{product.description}</p>
          <p className="muted">Application URL: {product.applicationUrl || "Not set"}</p>
          <p className="muted">Last updated {new Date(product.updatedAt).toLocaleString()}</p>
        </div>
      )}

      {tab === "branding" && (
        <div className="editor-layout">
          <div>
            <Alert message={formError} />
            <BrandEditor value={editorValue} themes={themes} onChange={setDraft} />
            <div className="actions" style={{ marginTop: 16 }}>
              <button className="button" type="button" onClick={() => void onSave()}>
                Save branding
              </button>
            </div>
          </div>
          <BrandPreview
            value={{
              productName: editorValue.displayName || product.name,
              shortName: editorValue.shortName,
              tagline: editorValue.tagline,
              logoUrl: editorValue.logoUrl || "/assets/hidden-wing/logo.svg",
              primaryColor: editorValue.primaryColor || selectedTheme?.primaryColor,
              secondaryColor: editorValue.secondaryColor || selectedTheme?.secondaryColor,
              accentColor: editorValue.accentColor || selectedTheme?.accentColor,
              backgroundColor: editorValue.backgroundColor || selectedTheme?.backgroundColor,
              surfaceColor: editorValue.surfaceColor || selectedTheme?.surfaceColor,
              textColor: editorValue.textColor || selectedTheme?.textColor,
              mutedTextColor: editorValue.mutedTextColor || selectedTheme?.mutedTextColor,
              borderColor: editorValue.borderColor || selectedTheme?.borderColor,
              fontFamily: editorValue.fontFamily || selectedTheme?.fontFamily
            }}
          />
        </div>
      )}

      {tab === "theme" && (
        <div className="panel stack">
          <p>Assigned theme: {selectedTheme?.name ?? "Hidden Wing default"}</p>
          <p className="muted">Override individual colors in the Branding tab. Empty fields inherit from this theme.</p>
        </div>
      )}

      {tab === "assets" && (
        <div className="panel stack">
          <p>Product assets are managed in the Assets page and can be scoped to this product.</p>
          <Link className="button-secondary" to="/assets">
            Open assets
          </Link>
        </div>
      )}

      {tab === "integration" && (
        <div className="panel stack">
          <p>Other Hidden Wing applications consume this product through the public branding API.</p>
          <code>
            GET {environment.apiBaseUrl}/api/v1/public/branding/{product.slug}
          </code>
        </div>
      )}
    </div>
  );
};
