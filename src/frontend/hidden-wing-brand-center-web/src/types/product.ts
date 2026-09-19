export type ProductStatus = "Active" | "Inactive";

export interface Product {
  id: string;
  brandId: string;
  brandName: string;
  name: string;
  slug: string;
  description?: string | null;
  applicationUrl?: string | null;
  isActive: boolean;
  status: ProductStatus;
  createdAt: string;
  updatedAt: string;
}

export interface ProductWriteModel {
  brandId: string;
  name: string;
  slug: string;
  description: string;
  applicationUrl: string;
  isActive: boolean;
}

export interface ProductBranding {
  id: string;
  productId: string;
  themeId?: string | null;
  displayName?: string | null;
  shortName?: string | null;
  tagline?: string | null;
  logoUrl?: string | null;
  logoDarkUrl?: string | null;
  logoLightUrl?: string | null;
  iconUrl?: string | null;
  faviconUrl?: string | null;
  primaryColor?: string | null;
  secondaryColor?: string | null;
  accentColor?: string | null;
  backgroundColor?: string | null;
  surfaceColor?: string | null;
  textColor?: string | null;
  mutedTextColor?: string | null;
  borderColor?: string | null;
  fontFamily?: string | null;
  supportEmail?: string | null;
  websiteUrl?: string | null;
  privacyUrl?: string | null;
  termsUrl?: string | null;
  isEnabled: boolean;
  createdAt?: string;
  updatedAt?: string;
}

export type ProductDetailTab = "overview" | "branding" | "theme" | "assets" | "integration";
