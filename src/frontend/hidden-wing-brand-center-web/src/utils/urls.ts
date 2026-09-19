import { environment } from "../config/environment";

export const resolveAssetUrl = (url: string | null | undefined): string => {
  if (!url) {
    return "/favicon.svg";
  }

  if (url.startsWith("http://") || url.startsWith("https://") || url.startsWith("data:")) {
    return url;
  }

  if (url.startsWith("/assets/")) {
    return url;
  }

  return `${environment.apiBaseUrl}${url}`;
};
