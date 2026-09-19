import { useCallback, useEffect, useState } from "react";
import { productApi } from "../api/productApi";
import { ApiError } from "../types/api";
import type { Product, ProductBranding, ProductWriteModel } from "../types/product";

export const useProduct = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const refresh = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await productApi.list();
      setProducts(response.data ?? []);
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to load products.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void refresh();
  }, [refresh]);

  const save = async (id: string | null, payload: ProductWriteModel) => {
    if (id) {
      await productApi.update(id, payload);
    } else {
      await productApi.create(payload);
    }
    await refresh();
  };

  const remove = async (id: string) => {
    await productApi.remove(id);
    await refresh();
  };

  return { products, loading, error, refresh, save, remove };
};

export const useProductDetail = (productId: string | undefined) => {
  const [product, setProduct] = useState<Product | null>(null);
  const [branding, setBranding] = useState<ProductBranding | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const refresh = useCallback(async () => {
    if (!productId) {
      return;
    }

    setLoading(true);
    setError(null);
    try {
      const [productResponse, brandingResponse] = await Promise.all([
        productApi.get(productId),
        productApi.getBranding(productId)
      ]);
      setProduct(productResponse.data ?? null);
      setBranding(brandingResponse.data ?? null);
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to load product.");
    } finally {
      setLoading(false);
    }
  }, [productId]);

  useEffect(() => {
    void refresh();
  }, [refresh]);

  const saveBranding = async (payload: ProductBranding) => {
    if (!productId) {
      return;
    }

    const response = await productApi.updateBranding(productId, payload);
    setBranding(response.data ?? payload);
  };

  return { product, branding, loading, error, refresh, saveBranding };
};
