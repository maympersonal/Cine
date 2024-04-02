// SearchBar.jsx
import React, { useState } from 'react';
import './SearchBar (2).css';
import axios from '../../api/axios';
import ClienteCard from './UserComponent (2)';
import { useUser } from '../../context/UserContext';

const SearchBar = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);
  const [searchedUser,setUser]=useState(null)
  const {user}=useUser();

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
      {(!loading&&searched)?<ClienteCard nombre={searchedUser.nombreS} estadoInicial={searchedUser.ciNavigation.confiabilidad} correo={searchedUser.ciNavigation.correo} ci={searchedUser.ci} rol={user.rol}/>:null}
      {loading && <div className="loading-message">Cargando...</div>}
    </div>
  );
};

export default SearchBar;
