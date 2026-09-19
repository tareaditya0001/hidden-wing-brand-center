import { render, screen } from "@testing-library/react";
import { BrandPreview } from "./BrandPreview";

describe("BrandPreview", () => {
  it("renders local preview state without an API call", () => {
    render(
      <BrandPreview
        value={{
          productName: "Hidden Wing Store",
          shortName: "Store",
          tagline: "Commerce under Hidden Wing",
          primaryColor: "#6B3FA0"
        }}
      />
    );

    expect(screen.getByText("Hidden Wing Store")).toBeInTheDocument();
    expect(screen.getByText("Commerce under Hidden Wing")).toBeInTheDocument();
    expect(screen.getByText("Primary action")).toBeInTheDocument();
  });
});
