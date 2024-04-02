// SearchBar.jsx
import React, { useState } from 'react';
import './SearchBar.css';

const SearchBar = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true); // Activar el estado de carga
    await onSearch(searchTerm); // Esperar a que se complete la búsqueda
    setLoading(false); // Desactivar el estado de carga
    setSearched(true); // Marcamos como realizada la búsqueda
  };

  return (
    <div className={`search-bar ${searched ? 'searched' : ''}`}>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Buscar..."
          value={searchTerm}
          onChange={handleChange}
        />
        <button type="submit">Buscar</button>
      </form>
      {loading && <div className="loading-message">Cargando...</div>}
    </div>
  );
};

export default SearchBar;
