import type { DashboardSummary, LoginResult } from "../types/api";
import { apiGet, apiSend } from "./client";

export const dashboardApi = {
  summary: () => apiGet<DashboardSummary>("/api/v1/dashboard")
};

export const authApi = {
  login: (username: string, password: string) =>
    apiSend<LoginResult>("/api/v1/auth/login", "POST", { username, password })
};
