import { FormEvent, useState } from "react";
import { Link } from "react-router-dom";
import { Alert } from "../components/common/Alert";
import { PageHeader } from "../components/common/PageHeader";
import { StatusBadge } from "../components/common/StatusBadge";
import { useBrand } from "../hooks/useBrand";
import { useProduct } from "../hooks/useProduct";
import { ApiError } from "../types/api";
import type { ProductWriteModel } from "../types/product";
import { slugify } from "../utils/validation";

const emptyProduct = (brandId = ""): ProductWriteModel => ({
  brandId,
  name: "",
  slug: "",
  description: "",
  applicationUrl: "",
  isActive: true
});

export const Products = () => {
  const { products, loading, error, save } = useProduct();
  const { brands } = useBrand();
  const [draft, setDraft] = useState<ProductWriteModel>(emptyProduct());
  const [formError, setFormError] = useState<string | null>(null);

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setFormError(null);
    try {
      await save(null, draft);
      setDraft(emptyProduct(brands[0]?.id ?? ""));
    } catch (caught) {
      setFormError(caught instanceof ApiError ? caught.message : "Unable to create product.");
    }
  };

  return (
    <div>
      <PageHeader title="Products" description="Applications that consume Hidden Wing branding." />
      <Alert message={error} />
      <div className="table-wrap">
        {loading ? (
          <p className="empty">Loading products...</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Product</th>
                <th>Slug</th>
                <th>Status</th>
                <th>Brand</th>
                <th>Last updated</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {products.map((product) => (
                <tr key={product.id}>
                  <td>{product.name}</td>
                  <td>{product.slug}</td>
                  <td>
                    <StatusBadge active={product.isActive} />
                  </td>
                  <td>{product.brandName}</td>
                  <td>{new Date(product.updatedAt).toLocaleDateString()}</td>
                  <td>
                    <Link className="button-secondary" to={`/products/${product.id}`}>
                      Configure
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
      <form className="panel stack" style={{ marginTop: 20 }} onSubmit={(event) => void onSubmit(event)}>
        <h2>Add product</h2>
        <Alert message={formError} />
        <div className="form-grid">
          <label>
            Brand
            <select
              value={draft.brandId || brands[0]?.id || ""}
              onChange={(event) => setDraft({ ...draft, brandId: event.target.value })}
            >
              {brands.map((brand) => (
                <option key={brand.id} value={brand.id}>
                  {brand.name}
                </option>
              ))}
            </select>
          </label>
          <label>
            Name
            <input
              value={draft.name}
              onChange={(event) => setDraft({ ...draft, name: event.target.value, slug: slugify(event.target.value) })}
            />
          </label>
          <label>
            Slug
            <input value={draft.slug} onChange={(event) => setDraft({ ...draft, slug: event.target.value })} />
          </label>
          <label>
            Application URL
            <input value={draft.applicationUrl} onChange={(event) => setDraft({ ...draft, applicationUrl: event.target.value })} />
          </label>
        </div>
        <button className="button" type="submit">
          Create product
        </button>
      </form>
    </div>
  );
};
