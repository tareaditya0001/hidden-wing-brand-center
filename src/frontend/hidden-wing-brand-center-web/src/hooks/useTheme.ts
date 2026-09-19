import { useCallback, useEffect, useState } from "react";
import { themeApi } from "../api/themeApi";
import { ApiError } from "../types/api";
import type { Theme, ThemeWriteModel } from "../types/theme";

export const useTheme = () => {
  const [themes, setThemes] = useState<Theme[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const refresh = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await themeApi.list();
      setThemes(response.data ?? []);
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to load themes.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void refresh();
  }, [refresh]);

  const save = async (id: string | null, payload: ThemeWriteModel) => {
    if (id) {
      await themeApi.update(id, payload);
    } else {
      await themeApi.create(payload);
    }
    await refresh();
  };

  const remove = async (id: string) => {
    await themeApi.remove(id);
    await refresh();
  };

  return { themes, loading, error, refresh, save, remove };
};
