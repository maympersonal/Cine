import React, { useState } from 'react';
import './UserComponent (2).css'; // Asegúrate de tener el archivo CSS para el estilo
import axios from '../../api/axios';

const ClienteCard = ({ nombre, estadoInicial ,ci,correo,rol,rolsearched}) => {
  const [estadoConfianza, setEstadoConfianza] = useState(estadoInicial);
  const [eliminado, setEliminado] = useState(false);
  
  const cambiarEstadoConfianza = async() => {
    await axios.put('/Cliente/Update/'+ci,{Ci:ci,Correo:correo,Confiabilidad:!estadoConfianza})
    .then(res=>{console.log(res.status)})
    .catch(error=>console.log(error+"    ayayayayayayaya"))
    setEstadoConfianza(!estadoConfianza);
  };

  const handleEliminar = async() => {
    if (!eliminado) {
      setEliminado(true);
      await axios.delete('/Usuario/Delete/'+ci)
      .then(res=>console.log(res))
      .catch(error=>console.log(error+"    ayayayayayayaya"))
    } 
  };

  return (
    <div className={`cliente-card ${eliminado ? 'eliminado' : ''}`}>
      <span className="nombre">{nombre}</span>
      <span className="estado-confianza">{estadoConfianza ? 'Confiable' : 'No Confiable'}</span>
      {!(rolsearched==="Admin"||rolsearched==="Taquillero")?<button onClick={cambiarEstadoConfianza}>
        Cambiar estado
      </button>:null}
      {rol==="Admin"?<button onClick={handleEliminar} className="eliminar-btn" disabled={eliminado}>
        { 'Eliminar'}
      </button>:null}
    </div>
  );
};

export default ClienteCard;
