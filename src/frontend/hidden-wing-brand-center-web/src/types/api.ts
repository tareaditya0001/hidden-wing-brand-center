export interface Pagination {
  page: number;
  pageSize: number;
  total: number;
}

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
  errorCode?: string;
  errors?: string[];
  pagination?: Pagination;
}

export interface DashboardSummary {
  activeBrands: number;
  activeProducts: number;
  themes: number;
  assets: number;
  recentChanges: RecentChange[];
}

export interface RecentChange {
  entityType: string;
  name: string;
  updatedAt: string;
}

export interface LoginResult {
  accessToken: string;
  tokenType: string;
  expiresInMinutes: number;
}

export class ApiError extends Error {
  public readonly status: number;
  public readonly errorCode?: string;
  public readonly errors: string[];

  public constructor(message: string, status: number, errorCode?: string, errors: string[] = []) {
    super(message);
    this.name = "ApiError";
    this.status = status;
    this.errorCode = errorCode;
    this.errors = errors;
  }
}
