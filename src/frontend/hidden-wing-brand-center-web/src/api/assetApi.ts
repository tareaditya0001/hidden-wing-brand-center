import type { BrandAsset } from "../types/asset";
import { apiGet, apiSend } from "./client";

export const assetApi = {
  list: (brandId?: string, productId?: string) => {
    const params = new URLSearchParams({ page: "1", pageSize: "100" });
    if (brandId) {
      params.set("brandId", brandId);
    }
    if (productId) {
      params.set("productId", productId);
    }

    return apiGet<BrandAsset[]>(`/api/v1/assets?${params.toString()}`);
  },
  create: (payload: Omit<BrandAsset, "id" | "createdAt" | "updatedAt">) =>
    apiSend<BrandAsset>("/api/v1/assets", "POST", payload),
  remove: (id: string) => apiSend<void>(`/api/v1/assets/${id}`, "DELETE")
};
