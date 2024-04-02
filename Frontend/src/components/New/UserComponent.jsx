import React, { useState } from 'react';
import './UserComponent.css'; // Asegúrate de tener el archivo CSS para el estilo

const ClienteCardd = ({ nombre, estadoInicial }) => {
  const [estadoConfianza, setEstadoConfianza] = useState(estadoInicial);

  const cambiarEstadoConfianza = () => {
    setEstadoConfianza(!estadoConfianza);
  };

  return (
    <div className="cliente-card">
      <p style={{display:'contents'}}>
        <span className="nombre">{nombre}</span>
        <span className="estado-confianza">{estadoConfianza ? 'Confiable' : 'No Confiable'}</span>
        <button onClick={cambiarEstadoConfianza}>
          Cambiar estado
        </button>
      </p>
    </div>
  );
};

export default ClienteCardd;
