import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';

import {ClassroomService} from '../../../shared-services/classroom/classroom.service';
import {Person} from '../../../models/api';
import {Classroom} from '../../../models/classroom';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {

  @Input() rol: number;
  @Input() classrom: any;
  @Input() person: Person;
  @Output() finish: EventEmitter<{finished: boolean}> = new EventEmitter<{finished: boolean}>();

  selectedClassroom: any;
  classrooms: any[];

  constructor(
    private classroomService: ClassroomService
  ) { }

  ngOnInit() {
    this.getClassrooms(1, 'adminsystem');
  }

  back() {
    this.finish.emit({finished: true});
  }

  save(){
    this.finish.emit({finished: true});
  }

  getClassrooms(
    user: number,
    rol: string
  ){
    this.classrooms = [
      {label: 'Seleccionar clase', code: '0'}
    ];
    this.classroomService.getClassrooms(user, rol)
      .subscribe(
        res => {
          res.map( classroom => {
            this.classrooms.push({label: classroom.Name, code: classroom})
          });
        }
      )
  }

}
