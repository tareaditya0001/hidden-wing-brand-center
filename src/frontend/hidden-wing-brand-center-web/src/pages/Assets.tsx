import { FormEvent, useEffect, useState } from "react";
import { assetApi } from "../api/assetApi";
import { Alert } from "../components/common/Alert";
import { PageHeader } from "../components/common/PageHeader";
import { useBrand } from "../hooks/useBrand";
import { useProduct } from "../hooks/useProduct";
import { ApiError } from "../types/api";
import { assetTypes, type AssetType, type BrandAsset } from "../types/asset";

export const Assets = () => {
  const { brands } = useBrand();
  const { products } = useProduct();
  const [assets, setAssets] = useState<BrandAsset[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [name, setName] = useState("");
  const [url, setUrl] = useState("");
  const [type, setType] = useState<AssetType>("Logo");
  const [brandId, setBrandId] = useState("");
  const [productId, setProductId] = useState("");

  const refresh = async () => {
    try {
      const response = await assetApi.list();
      setAssets(response.data ?? []);
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to load assets.");
    }
  };

  useEffect(() => {
    void refresh();
  }, []);

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setError(null);
    try {
      await assetApi.create({
        brandId: brandId || brands[0]?.id || "",
        productId: productId || null,
        name,
        type,
        url,
        fileSize: 0,
        isActive: true
      });
      setName("");
      setUrl("");
      await refresh();
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to create asset.");
    }
  };

  return (
    <div>
      <PageHeader title="Assets" description="Logo, favicon and supporting brand files." />
      <Alert message={error} />
      <form className="panel form-grid" onSubmit={(event) => void onSubmit(event)}>
        <label>
          Name
          <input value={name} onChange={(event) => setName(event.target.value)} />
        </label>
        <label>
          Type
          <select value={type} onChange={(event) => setType(event.target.value as AssetType)}>
            {assetTypes.map((item) => (
              <option key={item} value={item}>
                {item}
              </option>
            ))}
          </select>
        </label>
        <label>
          Brand
          <select value={brandId || brands[0]?.id || ""} onChange={(event) => setBrandId(event.target.value)}>
            {brands.map((brand) => (
              <option key={brand.id} value={brand.id}>
                {brand.name}
              </option>
            ))}
          </select>
        </label>
        <label>
          Product
          <select value={productId} onChange={(event) => setProductId(event.target.value)}>
            <option value="">Global brand</option>
            {products.map((product) => (
              <option key={product.id} value={product.id}>
                {product.name}
              </option>
            ))}
          </select>
        </label>
        <label>
          URL
          <input value={url} onChange={(event) => setUrl(event.target.value)} placeholder="/assets/hidden-wing/logo.svg" />
        </label>
        <div style={{ alignSelf: "end" }}>
          <button className="button" type="submit">
            Add asset
          </button>
        </div>
      </form>
      <div className="table-wrap" style={{ marginTop: 20 }}>
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Type</th>
              <th>URL</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {assets.map((asset) => (
              <tr key={asset.id}>
                <td>{asset.name}</td>
                <td>{asset.type}</td>
                <td>{asset.url}</td>
                <td>
                  <button className="button danger" type="button" onClick={() => void assetApi.remove(asset.id).then(refresh)}>
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
