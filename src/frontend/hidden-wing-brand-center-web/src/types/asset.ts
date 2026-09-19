export type AssetType =
  | "Logo"
  | "LogoDark"
  | "LogoLight"
  | "Icon"
  | "Favicon"
  | "OgImage"
  | "EmailLogo"
  | "Other";

export interface BrandAsset {
  id: string;
  brandId: string;
  productId?: string | null;
  name: string;
  type: AssetType;
  url: string;
  mimeType?: string | null;
  fileSize: number;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export const assetTypes: AssetType[] = [
  "Logo",
  "LogoDark",
  "LogoLight",
  "Icon",
  "Favicon",
  "OgImage",
  "EmailLogo",
  "Other"
];
