import { FormEvent, useState } from "react";
import { Alert } from "../components/common/Alert";
import { PageHeader } from "../components/common/PageHeader";
import { StatusBadge } from "../components/common/StatusBadge";
import { useBrand } from "../hooks/useBrand";
import { ApiError } from "../types/api";
import type { Brand, BrandWriteModel } from "../types/brand";
import { slugify } from "../utils/validation";

const emptyBrand = (): BrandWriteModel => ({
  name: "",
  slug: "",
  companyName: "",
  description: "",
  isActive: true
});

export const Brands = () => {
  const { brands, loading, error, save, remove } = useBrand();
  const [draft, setDraft] = useState<BrandWriteModel>(emptyBrand());
  const [editingId, setEditingId] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [formError, setFormError] = useState<string | null>(null);

  const beginEdit = (brand: Brand) => {
    setEditingId(brand.id);
    setDraft({
      name: brand.name,
      slug: brand.slug,
      companyName: brand.companyName,
      description: brand.description ?? "",
      isActive: brand.isActive
    });
  };

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setFormError(null);
    try {
      await save(editingId, draft);
      setMessage("Brand saved.");
      setEditingId(null);
      setDraft(emptyBrand());
    } catch (caught) {
      setFormError(caught instanceof ApiError ? caught.message : "Unable to save brand.");
    }
  };

  return (
    <div>
      <PageHeader title="Brands" description="Company-level identities that products inherit." />
      <Alert message={error} />
      <Alert kind="success" message={message} />
      <div className="grid-2">
        <form className="panel stack" onSubmit={(event) => void onSubmit(event)}>
          <h2>{editingId ? "Edit brand" : "New brand"}</h2>
          <Alert message={formError} />
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
          <label>
            Company name
            <input value={draft.companyName} onChange={(event) => setDraft({ ...draft, companyName: event.target.value })} />
          </label>
          <label>
            Description
            <textarea value={draft.description} onChange={(event) => setDraft({ ...draft, description: event.target.value })} />
          </label>
          <div className="actions">
            <button className="button" type="submit">
              Save
            </button>
            <button
              className="button-secondary"
              type="button"
              onClick={() => {
                setEditingId(null);
                setDraft(emptyBrand());
              }}
            >
              Clear
            </button>
          </div>
        </form>
        <div className="table-wrap">
          {loading ? (
            <p className="empty">Loading brands...</p>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Slug</th>
                  <th>Status</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {brands.map((brand) => (
                  <tr key={brand.id}>
                    <td>{brand.name}</td>
                    <td>{brand.slug}</td>
                    <td>
                      <StatusBadge active={brand.isActive} />
                    </td>
                    <td className="actions">
                      <button className="button-secondary" type="button" onClick={() => beginEdit(brand)}>
                        Edit
                      </button>
                      <button className="button danger" type="button" onClick={() => void remove(brand.id)}>
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
  );
};
