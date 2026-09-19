import type { ReactElement } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import { tokenStore } from "./api/client";
import { AppLayout } from "./layouts/AppLayout";
import { Assets } from "./pages/Assets";
import { Brands } from "./pages/Brands";
import { Dashboard } from "./pages/Dashboard";
import { Login } from "./pages/Login";
import { ProductDetail } from "./pages/ProductDetail";
import { Products } from "./pages/Products";
import { Settings } from "./pages/Settings";
import { Themes } from "./pages/Themes";

const RequireAuth = ({ children }: { children: ReactElement }) => {
  return tokenStore.get() ? children : <Navigate to="/login" replace />;
};

export const App = () => {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route
        element={
          <RequireAuth>
            <AppLayout />
          </RequireAuth>
        }
      >
        <Route path="/" element={<Dashboard />} />
        <Route path="/brands" element={<Brands />} />
        <Route path="/products" element={<Products />} />
        <Route path="/products/:id" element={<ProductDetail />} />
        <Route path="/themes" element={<Themes />} />
        <Route path="/assets" element={<Assets />} />
        <Route path="/settings" element={<Settings />} />
      </Route>
    </Routes>
  );
};
