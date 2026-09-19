import { ApiError } from "../types/api";
import { apiRequest, tokenStore } from "./client";

describe("api client error handling", () => {
  beforeEach(() => {
    tokenStore.clear();
    vi.stubGlobal(
      "fetch",
      vi.fn().mockResolvedValue({
        ok: false,
        status: 400,
        text: async () =>
          JSON.stringify({
            success: false,
            message: "Validation failed.",
            errorCode: "VALIDATION_FAILED",
            errors: ["Slug must be lowercase letters, numbers, and hyphens."]
          })
      })
    );
  });

  afterEach(() => {
    vi.unstubAllGlobals();
  });

  it("throws a typed ApiError from the standard envelope", async () => {
    await expect(apiRequest("/api/v1/brands", { method: "POST", body: "{}" })).rejects.toMatchObject({
      name: "ApiError",
      status: 400,
      errorCode: "VALIDATION_FAILED"
    } satisfies Partial<ApiError>);
  });
});
