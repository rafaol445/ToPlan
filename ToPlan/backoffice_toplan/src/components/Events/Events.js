import React, {Component, useState} from "react";
import axios from "axios";
import {Button} from "primereact/button";
import {InputText} from "primereact/inputtext";
import "./StyleEvents.css";
import {TabPanel, TabView} from "primereact/tabview";
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/saga-blue/theme.css';
import 'primereact/resources/primereact.css';
import {Dropdown} from "primereact/dropdown";
import {DataTable} from "primereact/datatable";
import {Column} from "primereact/column";
import {InputTextarea} from "primereact/inputtextarea";
import {InputMask} from "primereact/inputmask";

export class Events extends Component {

    constructor(props) {
        super(props);
        this.state = {
            EventId: '',
            Events: [],
            date: '',
            city: '',
            province: '',
            description: '',
            TypePlanName: '',
            Members: '',
            UserId: '',
            typeName: '',
            Data: '',
            isHidden: false,
            ListMembers: '',
            direction: '',
            TypePlanArray: [],
            TypePlanId: '',
        }
    }


    componentDidMount() {
        axios.get("http://3.95.8.159:44360/api/TypePlan/List", {}, {headers: {'Access-Control-Allow-Origin': '*'}}).then((response => {
            console.log(response)
            this.setState({TypePlanArray: response.data})
        })).catch((error => {
            console.log(error)
        }));
    }


    getEvents = () => {
        axios.get('http://3.95.8.159:44360/api/Event').then((respuesta) => {
            this.setState({Events: respuesta.data});
            console.log(respuesta.data);
        }).catch(e => {
            console.log("Error de conexion con la API");
        });
    }


    onSubmitInsertEvent = () => {
        const Type = this.state.TypePlanId[0];
        const TypePlanId = parseInt(Type);
        console.log(TypePlanId);

        /*axios.post('http://3.95.8.159:44360/api/Event', {
            UserId: this.state.UserId,
            EventDate: this.state.date,
            City: this.state.city,
            Province: this.state.province,
            Description: this.state.description,
            MaxMembers: this.state.Members,
            TypePlanId: TypePlanId,
            ListMembers: this.state.UserId,
            Direccion: this.state.direction
        })
            .then((response) => {
                console.log(response);
            }, (error) => {
                console.log(error);
            })*/
    }

    onSubmitUpdate = () => {

        const promiseUpdate = axios.put("http://3.95.8.159:44360/api/Event/Put/id=" + this.state.EventId + "&dir=" + this.state.direction + "&f=" + this.state.date + "&c=" + this.state.city + "&p=" + this.state.province + "&d=" + this.state.description + "&max=" + this.state.maxMembers, {}, {headers: {'Access-Control-Allow-Origin': '*'}}
        ).then(response => {
            console.log("Update succesfully: " + response)
        }).catch(e => {
            console.log(e);
        });

    }


    onInputEventId = (event) => {
        this.setState({
            EventId: event.target.value
        });
    }

    onInputDate = (event) => {
        this.setState({
            date: event.target.value
        });
    }

    onInputCity = (event) => {
        this.setState({
            city: event.target.value
        });
    }

    onInputProvince = (event) => {
        this.setState({
            province: event.target.value
        });
    }

    onInputDescription = (event) => {
        this.setState({
            description: event.target.value
        });
    }

    onInputUserId = (event) => {
        this.setState({
            UserId: event.target.value
        });
    }

    onInputMembers = (event) => {
        this.setState({
            Members: event.target.value
        });
    }

    onSubmitDeleteEvent = () => {

        let promisePost = axios.delete("http://3.95.8.159:44360/api/Event?id=" + this.state.EventId, {}, {headers: {'Access-Control-Allow-Origin': '*'}}
        ).then(response => {
            console.log("Delete successfully")
            window.location.reload(true);
        }).catch(e => {
            console.log(e)

        });
    }


    onInputChange = (event) => {
        this.setState({
            EventId: event.target.value
        });
    }

    onInputChangeDirection = (event) => {
        this.setState({
            direction: event.target.value
        });
    }

    onInputChangeType = (event) => {
        this.setState({TypePlanId: event.target.value});
    }

    onInputTypeFilter = (event) => {
        let aux = event.target.value;
        let aux2 = aux.split(" ", 1).toString();

        this.setState({TypePlanName: aux2});
        this.setState({Data: event.target.value});
    }

    checkerEvent = () => {
        const promise = axios.get('http://3.95.8.159:44360/api/Event/Check?id=' + this.state.EventId, {headers: {'Access-Control-Allow-Origin': '*'}})
        const promiseResult = promise.then((resolveResult) => {
                if (resolveResult.data === true) {
                    console.log(resolveResult.data);
                    this.setState({isHidden: !this.state.isHidden})

                } else {
                    console.log("No existe ese evento id")
                }
            }
            , (rejectedResult) => {
                console.error(rejectedResult.statusText)
            });
    }

    GetEvents = () => {
        axios.get('http://3.95.8.159:44360/api/Event', {headers: {'Access-Control-Allow-Origin': '*'}}).then((respuesta) => {
            this.setState({Events: respuesta.data});
            console.log(this.state.Events);
        }).catch(e => {
            console.log("Error de conexion con la API");
        });
    }

    filterType = () => {
        axios.get('http://3.95.8.159:44360/api/Event/CheckType?id=' + this.state.TypePlanName, {headers: {'Access-Control-Allow-Origin': '*'}}).then((respuesta) => {
            if (respuesta.data === true) {
                axios.get('http://3.95.8.159:44360/api/Eventtype?id=' + this.state.TypePlanName, {headers: {'Access-Control-Allow-Origin': '*'}}).then
                (response => {
                        this.setState({Events: response.data})
                    console.log(response.data);
                    }
                )
            } else {
                alert("No existe ese Tipo de Plan");
            }
        }).catch(e => {
            console.log("Error de conexion con la API");
        });
    }

    render() {
        return (
            <div className="groupUpdate">
                <div className="tabview-demo">
                    <div className="card">
                        <TabView>
                            <TabPanel header="Modificar">
                                <div className="update">
                                    <h5>Update Event</h5>
                                    <InputText placeholder={"EventId"} type={'number'}
                                               name="EventId" value={this.state.EventId}
                                               onChange={this.onInputEventId}
                                    />
                                    <Button style={{marginLeft: 10}} onClick={this.checkerEvent}
                                            icon={'pi pi-check'}/>
                                    <div hidden={!this.state.isHidden}>
                                        <InputMask id="date" type={'text'} mask="9999-99-99" value={this.state.date}
                                                   placeholder="yyyy/mm/dd" slotChar="yyyy/mm/dd"
                                                   onChange={this.onInputDate}/>

                                        <InputText placeholder={"EventCity"} type={'text'}
                                                   name="EventDate" value={this.state.city}
                                                   onChange={this.onInputCity}
                                        />
                                        <InputText placeholder={"Direccion"} type={'text'}
                                                   name="Direccion" value={this.state.direction}
                                                   onChange={this.onInputChangeDirection}
                                        />
                                        <InputText placeholder={"EventProvince"} type={'text'}
                                                   name="EventDate" value={this.state.province}
                                                   onChange={this.onInputProvince}
                                        />
                                        <p>
                                            <InputTextarea placeholder={"EventDescription"} rows={5}
                                                           cols={30} name="EventDate" value={this.state.description}
                                                           onChange={this.onInputDescription}
                                            />
                                        </p>
                                        <InputText placeholder={"Event Members"} type={'number'}
                                                   name="Event Members" value={this.state.Members}
                                                   onChange={this.onInputMembers}
                                        />
                                        <p>
                                            <Button label={"Update Event"} onClick={this.onSubmitUpdate}/>
                                        </p>
                                    </div>

                                </div>
                            </TabPanel>
                            <TabPanel header="Insertar">
                                <div className="update">
                                    <h5>Insert Event</h5>
                                    <div>
                                        <InputText placeholder={"UserId"} type={'text'}
                                                   name="UserId" value={this.state.UserId}
                                                   onChange={this.onInputUserId}
                                        />

                                        <InputMask id="date" type={'text'} mask="9999-99-99" value={this.state.date}
                                                   placeholder="Date Event" slotChar="yyyy/mm/dd"
                                                   onChange={this.onInputDate}/>

                                        <InputText placeholder={"Event Direction"} type={'text'}
                                                   name="Direccion" value={this.state.direction}
                                                   onChange={this.onInputChangeDirection}
                                        />
                                        <InputText placeholder={"EventCity"} type={'text'}
                                                   name="EventDate" value={this.state.city}
                                                   onChange={this.onInputCity}
                                        />
                                        <InputText placeholder={"EventProvince"} type={'text'}
                                                   name="EventDate" value={this.state.province}
                                                   onChange={this.onInputProvince}
                                        />
                                        <p>
                                            <InputTextarea placeholder={"EventDescription"} rows={5}
                                                           cols={30}
                                                           name="EventDate" value={this.state.description}
                                                           onChange={this.onInputDescription}
                                            />

                                        </p>
                                        <InputText placeholder={"Event Members"} type={'number'}
                                                   name="Event Members" value={this.state.Members}
                                                   onChange={this.onInputMembers}
                                        />
                                        <Dropdown autoWidth={true} value={this.state.TypePlanId}
                                                  options={this.state.TypePlanArray.map(elem => {
                                                      return elem.TypePlanId + " - " + elem.Subtype
                                                  })}
                                                  onChange={this.onInputChangeType}
                                                  placeholder="Select a Subtype Plan"/>
                                    </div>
                                    <p><Button label={"Insert Event"} onClick={this.onSubmitInsertEvent}/></p>
                                </div>
                            </TabPanel>
                            <TabPanel header="Eliminar">
                                <div className="update">
                                    <h5>Delete Event</h5>
                                    <div>
                                        <InputText placeholder={"EventId"} type={'number'}
                                                   name="eventIdDelete" value={this.state.EventId}
                                                   onChange={this.onInputChange}/>

                                    </div>
                                    <p><Button label={"Delete Event"} onClick={this.onSubmitDeleteEvent}/></p>
                                </div>
                            </TabPanel>
                            <TabPanel header="Filtrar">
                                <div className="InputFiltrado">
                                    <label>Get all Events: </label>
                                    <Button style={{margin: 10}} onClick={this.getEvents}
                                            icon={'pi pi-check'}/>
                                    <label>Filtrar por el tipo de evento: </label>
                                    <Dropdown autoWidth={true} value={this.state.Data}
                                              options={this.state.TypePlanArray.map(elem => (elem.Name + " - " + elem.Subtype))}
                                              onChange={this.onInputTypeFilter} placeholder="Select a Type Plan"/>
                                    <Button style={{marginLeft: 10}} onClick={this.filterType}
                                            icon={'pi pi-check'}/>
                                </div>
                                <div className="DataTable">
                                    <DataTable value={this.state.Events}>
                                        <Column field="EventId" header="EventId"/>
                                        <Column field="EventDate" header="Date"/>
                                        <Column field="City" header="City"/>
                                        <Column field="Direccion" header="Direccion"/>
                                        <Column field="Province" header="Province"/>
                                        <Column field="Description" header="Description"/>
                                        <Column field="UserId" header="UserId"/>
                                        <Column field="TypePlanId" header="TypeId"/>
                                        <Column field="ListMembers" header="List Members"/>
                                        <Column field="MaxMembers" header="Max Members"/>
                                    </DataTable>
                                </div>
                            </TabPanel>
                        </TabView>
                    </div>
                </div>
            </div>


        )
    }
}