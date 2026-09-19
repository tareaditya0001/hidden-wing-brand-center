export interface Brand {
  id: string;
  name: string;
  slug: string;
  companyName: string;
  description?: string | null;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface BrandWriteModel {
  name: string;
  slug: string;
  companyName: string;
  description: string;
  isActive: boolean;
}
