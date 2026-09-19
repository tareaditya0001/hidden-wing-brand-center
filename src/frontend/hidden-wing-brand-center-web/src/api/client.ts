import { environment } from "../config/environment";
import { ApiError, type ApiResponse } from "../types/api";

const TOKEN_KEY = "hwbc.accessToken";

export const tokenStore = {
  get(): string | null {
    return window.localStorage.getItem(TOKEN_KEY);
  },
  set(token: string): void {
    window.localStorage.setItem(TOKEN_KEY, token);
  },
  clear(): void {
    window.localStorage.removeItem(TOKEN_KEY);
  }
};

async function parseJson<T>(response: Response): Promise<T | ApiResponse<T> | null> {
  const text = await response.text();
  if (!text) {
    return null;
  }

  return JSON.parse(text) as T | ApiResponse<T>;
}

function isApiResponse<T>(value: unknown): value is ApiResponse<T> {
  return typeof value === "object" && value !== null && "success" in value;
}

export async function apiRequest<T>(path: string, init: RequestInit = {}): Promise<ApiResponse<T>> {
  const headers = new Headers(init.headers);
  if (!headers.has("Content-Type") && init.body && !(init.body instanceof FormData)) {
    headers.set("Content-Type", "application/json");
  }

  const token = tokenStore.get();
  if (token) {
    headers.set("Authorization", `Bearer ${token}`);
  }

  const response = await fetch(`${environment.apiBaseUrl}${path}`, {
    ...init,
    headers
  });

  const payload = await parseJson<T>(response);

  if (response.status === 401 && !path.startsWith("/api/v1/auth")) {
    tokenStore.clear();
  }

  if (!response.ok) {
    if (isApiResponse<T>(payload)) {
      throw new ApiError(payload.message ?? "Request failed.", response.status, payload.errorCode, payload.errors ?? []);
    }

    throw new ApiError("Request failed.", response.status);
  }

  if (isApiResponse<T>(payload)) {
    return payload;
  }

  return { success: true, data: payload as T };
}

export async function apiGet<T>(path: string): Promise<ApiResponse<T>> {
  return apiRequest<T>(path);
}

export async function apiSend<T>(path: string, method: string, body?: unknown): Promise<ApiResponse<T>> {
  return apiRequest<T>(path, {
    method,
    body: body === undefined ? undefined : JSON.stringify(body)
  });
}
