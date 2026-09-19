import { useCallback, useEffect, useState } from "react";
import { brandApi } from "../api/brandApi";
import { ApiError } from "../types/api";
import type { Brand, BrandWriteModel } from "../types/brand";

export const useBrand = () => {
  const [brands, setBrands] = useState<Brand[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const refresh = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await brandApi.list();
      setBrands(response.data ?? []);
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to load brands.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void refresh();
  }, [refresh]);

  const save = async (id: string | null, payload: BrandWriteModel) => {
    if (id) {
      await brandApi.update(id, payload);
    } else {
      await brandApi.create(payload);
    }
    await refresh();
  };

  const remove = async (id: string) => {
    await brandApi.remove(id);
    await refresh();
  };

  return { brands, loading, error, refresh, save, remove };
};
