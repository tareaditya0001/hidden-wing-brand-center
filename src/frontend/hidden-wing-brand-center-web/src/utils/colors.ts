const hexPattern = /^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$/;

export const isHexColor = (value: string): boolean => hexPattern.test(value.trim());

export const fallbackColor = (value: string | null | undefined, fallback: string): string => {
  return value && isHexColor(value) ? value : fallback;
};
