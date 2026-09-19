import type { Brand, BrandWriteModel } from "../types/brand";
import { apiGet, apiSend } from "./client";

export const brandApi = {
  list: () => apiGet<Brand[]>("/api/v1/brands?page=1&pageSize=100"),
  get: (id: string) => apiGet<Brand>(`/api/v1/brands/${id}`),
  create: (payload: BrandWriteModel) => apiSend<Brand>("/api/v1/brands", "POST", payload),
  update: (id: string, payload: BrandWriteModel) => apiSend<Brand>(`/api/v1/brands/${id}`, "PUT", payload),
  remove: (id: string) => apiSend<void>(`/api/v1/brands/${id}`, "DELETE")
};
