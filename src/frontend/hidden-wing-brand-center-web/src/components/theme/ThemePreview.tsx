import { BrandPreview } from "../brand/BrandPreview";
import type { ThemeWriteModel } from "../../types/theme";

interface ThemePreviewProps {
  value: ThemeWriteModel;
}

export const ThemePreview = ({ value }: ThemePreviewProps) => {
  return (
    <BrandPreview
      value={{
        productName: value.name || "Theme preview",
        shortName: value.slug || "Theme",
        tagline: "Palette, type and radius",
        primaryColor: value.primaryColor,
        secondaryColor: value.secondaryColor,
        accentColor: value.accentColor,
        backgroundColor: value.backgroundColor,
        surfaceColor: value.surfaceColor,
        textColor: value.textColor,
        mutedTextColor: value.mutedTextColor,
        borderColor: value.borderColor,
        fontFamily: value.fontFamily
      }}
    />
  );
};
