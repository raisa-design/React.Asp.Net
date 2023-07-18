import React, { useState } from 'react';
import TitlePage from '../../TitlePage';
import {useHistory} from 'react-router-dom';
import { Button, FormControl, InputGroup } from 'react-bootstrap';


const clientes =[
{
  id: 1,
  nome: 'microsoft',
  responsavel: 'Otto',
  contato:106655,
  situacao:'Ativo'

},

{
  id: 2,
  nome: 'amazon',
  responsavel: 'Jonas',
  contato:106655,
  situacao:'Ativo'

},

{
  id: 3,
  nome: 'Google',
  responsavel: 'Noah',
  contato:106655,
  situacao:'Em análise'

},

{
  id: 4,
  nome: 'Facebook',
  responsavel: 'Otto',
  contato:106655,
  situacao:'Ativo'

},

{
  id: 5,
  nome: 'Twitter',
  responsavel: 'Michael',
  contato:106655,
  situacao:'Ativo'

}
]

export default function ClienteLista() {
    const history = useHistory();
    const[termoBusca, setTermoBusca] = useState('');
    const handleInputChange = (e) => {
       setTermoBusca(e.target.value);
       console.log(termoBusca)
    }

    const clientesFiltrados = clientes.filter((cliente)=>{
      return Object. values(cliente)
          .join('')
          .toLowerCase()
          .includes(termoBusca.toLowerCase());


    });
    const novoCliente = () => {
      history.push('/cliente/detalhe');
    }

  return (
        <>
        <TitlePage title='Cliente Lista'>
          <Button variant='outline-secondary' onClick={novoCliente}>
              <i className='fas fa-plus me-2'></i>
              Novo Cliente
          </Button>
        </TitlePage>
       < InputGroup className='mt-3 mb-3'>
            <InputGroup.Text>Buscar</InputGroup.Text>
            <FormControl onChange={handleInputChange} placeholder='Buscar por nome do cliente'/>
       </InputGroup>
        <table className="table table-striped table-hover">
  <thead className='table-dark mt-3'>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Nome</th>
      <th scope="col">Responsável</th>
      <th scope="col">Contato</th>
      <th scope="col">Situação</th>
      <th scope="col">Opções</th>
    </tr>
  </thead>
  <tbody>
    {clientesFiltrados.map((cliente)=>
    
      <tr key={cliente.id}>
          <td>{cliente.id}</td>
          <td>{cliente.nome}</td>
          <td>{cliente.responsavel}</td>
          <td>{cliente.contato}</td>
          <td>{cliente.situacao}</td>
          <td>
            <div>
                <button 
                  className=" btn btn-sm btn-outiline-primary me-2"
                  onClick={()=>
                    history.push(
                      `/cliente/detalhe/${cliente.id}`
                    )
                  }
                
                >
                  <i className='fas fa-user-edit  me-2'></i>
                  Editar
                </button>
                
                <button className=" btn btn-sm btn-outiline-danger me-2">
                <i className='fas fa-user-edit  me-2'></i>
                  Desativar
                </button>
            </div>
          </td>
          <td>
            <div>

            </div>
          </td>
      </tr>
    )}
    
  </tbody>
</table>
      </>
    
  );
}
