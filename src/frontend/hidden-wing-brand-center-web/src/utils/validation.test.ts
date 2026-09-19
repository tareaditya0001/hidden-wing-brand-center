import { isValidSlug, slugify } from "./validation";

describe("validation", () => {
  it("accepts product slugs", () => {
    expect(isValidSlug("store")).toBe(true);
    expect(isValidSlug("hidden-wing")).toBe(true);
  });

  it("rejects invalid slugs", () => {
    expect(isValidSlug("Hidden Wing")).toBe(false);
    expect(isValidSlug("STORE")).toBe(false);
  });

  it("slugifies display names", () => {
    expect(slugify("Hidden Wing Store")).toBe("hidden-wing-store");
  });
});
