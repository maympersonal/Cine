// SearchBar.jsx
import React, { useState } from 'react';
import './SearchBar.css';
import axios from '../api/axios';
import ClienteCard from './UserComponent (2)';

const SearchBar = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);
  const [user,setUser]=useState(null)

  const handleChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true); // Activar el estado de carga
    await axios.get('/Usuario/GetById/'+searchTerm)// Esperar a que se complete la búsqueda
    .then(response=>{setUser(response.data)})
    .catch(error => console.log(error + " Que clase de locuraaa")); 
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
      {(!loading&&searched)?<ClienteCard nombre={user.nombreS} estadoInicial={user.ciNavigation.confiabilidad}/>:null}
      {loading && <div className="loading-message">Cargando...</div>}
    </div>
  );
};

export default SearchBar;
