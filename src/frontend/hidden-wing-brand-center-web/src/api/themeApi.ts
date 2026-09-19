import type { Theme, ThemeWriteModel } from "../types/theme";
import { apiGet, apiSend } from "./client";

export const themeApi = {
  list: () => apiGet<Theme[]>("/api/v1/themes?page=1&pageSize=100"),
  get: (id: string) => apiGet<Theme>(`/api/v1/themes/${id}`),
  create: (payload: ThemeWriteModel) => apiSend<Theme>("/api/v1/themes", "POST", payload),
  update: (id: string, payload: ThemeWriteModel) => apiSend<Theme>(`/api/v1/themes/${id}`, "PUT", payload),
  remove: (id: string) => apiSend<void>(`/api/v1/themes/${id}`, "DELETE")
};
