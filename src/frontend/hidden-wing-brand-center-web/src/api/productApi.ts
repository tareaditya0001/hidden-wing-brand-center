import type { Product, ProductBranding, ProductWriteModel } from "../types/product";
import { apiGet, apiSend } from "./client";

export const productApi = {
  list: () => apiGet<Product[]>("/api/v1/products?page=1&pageSize=100"),
  get: (id: string) => apiGet<Product>(`/api/v1/products/${id}`),
  create: (payload: ProductWriteModel) => apiSend<Product>("/api/v1/products", "POST", payload),
  update: (id: string, payload: ProductWriteModel) => apiSend<Product>(`/api/v1/products/${id}`, "PUT", payload),
  remove: (id: string) => apiSend<void>(`/api/v1/products/${id}`, "DELETE"),
  getBranding: (id: string) => apiGet<ProductBranding>(`/api/v1/products/${id}/branding`),
  updateBranding: (id: string, payload: ProductBranding) =>
    apiSend<ProductBranding>(`/api/v1/products/${id}/branding`, "PUT", payload)
};
