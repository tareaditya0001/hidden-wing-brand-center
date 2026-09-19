interface ColorFieldProps {
  label: string;
  value: string;
  onChange: (value: string) => void;
}

export const ColorField = ({ label, value, onChange }: ColorFieldProps) => {
  return (
    <label>
      {label}
      <div className="color-field">
        <input type="color" value={value || "#1C3353"} onChange={(event) => onChange(event.target.value)} />
        <input value={value} onChange={(event) => onChange(event.target.value)} />
      </div>
    </label>
  );
};
