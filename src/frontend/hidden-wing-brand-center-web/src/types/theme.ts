export interface Theme {
  id: string;
  name: string;
  slug: string;
  primaryColor: string;
  secondaryColor: string;
  accentColor: string;
  backgroundColor: string;
  surfaceColor: string;
  textColor: string;
  mutedTextColor: string;
  borderColor: string;
  fontFamily: string;
  borderRadius: string;
  isDefault: boolean;
  createdAt: string;
  updatedAt: string;
}

export type ThemeWriteModel = Omit<Theme, "id" | "createdAt" | "updatedAt">;

export const emptyTheme = (): ThemeWriteModel => ({
  name: "",
  slug: "",
  primaryColor: "#1C3353",
  secondaryColor: "#3E536B",
  accentColor: "#C6A15B",
  backgroundColor: "#F4EFE6",
  surfaceColor: "#FFFFFF",
  textColor: "#1A1F29",
  mutedTextColor: "#5C6B7A",
  borderColor: "#D9D2C5",
  fontFamily: "Inter, system-ui, sans-serif",
  borderRadius: "8px",
  isDefault: false
});
