import React, { useState } from 'react';
import './UserComponent (2).css'; // Asegúrate de tener el archivo CSS para el estilo

const ClienteCard = ({ nombre, estadoInicial }) => {
  const [estadoConfianza, setEstadoConfianza] = useState(estadoInicial);
  const [eliminado, setEliminado] = useState(false);

  const cambiarEstadoConfianza = () => {
    setEstadoConfianza(!estadoConfianza);
  };

  const handleEliminar = () => {
    if (!eliminado) {
      setEliminado(true);
    } else {
      setEliminado(false);
    }
  };

  return (
    <div className={`cliente-card ${eliminado ? 'eliminado' : ''}`}>
      <span className="nombre">{nombre}</span>
      <span className="estado-confianza">{estadoConfianza ? 'Confiable' : 'No Confiable'}</span>
      <button onClick={cambiarEstadoConfianza}>
        Cambiar estado
      </button>
      <button onClick={handleEliminar} className="eliminar-btn">
        {eliminado ? 'Deshacer' : 'Eliminar'}
      </button>
    </div>
  );
};

export default ClienteCard;
